using System.Text.Json;
using Mcm.Interactions.Application.Features.Interactions.Commands.CreateInteraction;
using Mcm.Interactions.Application.Features.Interactions.Commands.DeleteInteraction;
using Mcm.Interactions.Application.Features.Interactions.Commands.UpdateInteraction;
using Mcm.Interactions.Application.Features.Interactions.Queries.GetAllInteraction;
using Mcm.Interactions.Application.Features.Interactions.Queries.GetInteraction;
using Mcm.Interactions.Application.Features.Interactions.Queries.GetInteractionAttachment;
using Mcm.Interactions.Application.Features.Reports.Commands.CreateReport;
using Mcm.Interactions.Application.Features.Reports.Queries.GetReport;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Interactions.Presentation.Controllers;

[ApiController]
[Route("api/interactions")]
public class InteractionController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HasAuthorization(PermModule.Interaction, PermAction.Create)]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateInteractionResponse>>> Create(
        [FromForm] CreateInteractionRequest body)
    {
        List<CreateInteraction_Information>? values = null;
        if (body.Informations is not null)
            values = JsonSerializer.Deserialize<List<CreateInteraction_Information>>(body.Informations!, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        var dates = JsonSerializer.Deserialize<CreateInteraction_Date>(body.Date, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        var reminder = JsonSerializer.Deserialize<CreateInteraction_Reminder>(body.Reminder, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        var participant = JsonSerializer.Deserialize<CreateInteraction_Participant>(body.Participant, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        var res = await _mediator.Send(new CreateInteractionCommand
        {
            Title = body.Title,
            TypeId = body.TypeId,
            Note = body.Note,
            StartDate = dates!.StartDate,
            EndDate = dates!.EndDate,
            ReminderType = reminder!.Type,
            ReminderValue = reminder.Value,
            ReminderRepeat = reminder.Repeat,
            TeamMembers = participant!.Members,
            Contacts = participant.Contacts,
            Informations = values,
            Attachments = body.Attachments ?? []
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.Interaction, PermAction.Read)]
    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetInteractionResponse>>> GetById(        
        Guid Id)
    {
        var res = await _mediator.Send(new GetInteractionQuery(
            new GetInteractionRequest(Id)
        ));
        return Ok(res);
    }

    [HasAuthorization(PermModule.Interaction, PermAction.Read)]
    [HttpGet("{Id}/attachments/{fileName}")]
    public async Task<IActionResult> GetAttachment(Guid Id, string fileName)
    {
        var res = await _mediator.Send(new GetInteractionAttachmentQuery(
            new GetInteractionAttachmentRequest(Id, fileName)));
        return File(res.Data!.Stream, res.Data.ContentType);
    }

    [HasAuthorization(PermModule.Interaction, PermAction.Update)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateInteractionResponse>>> Update(
        Guid Id, [FromForm] UpdateInteractionRequest body)
    {
        var res = await _mediator.Send(new UpdateInteractionCommand
        {
            Id = Id,
            Title = body.Title,
            Note = body.Note,
            StartDate = body.Date.StartDate,
            EndDate = body.Date.EndDate,
            ReminderType = body.Reminder.Type,
            ReminderValue = body.Reminder.Value,
            ReminderRepeat = body.Reminder.Repeat,
            Contacts = body.Participant.Contacts,
            TeamMembers = body.Participant.Members,
            Attachments = body.Attachments,
            OldUrls = body.OldUrls
            // Informations = body.Informations
        });
        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllInteractionResponse>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] Guid? TeamMemberId,
        [FromQuery] Guid? ContactId,
        [FromQuery] DateTime? StartDateFrom,
        [FromQuery] DateTime? StartDateTo,
        [FromQuery] DateTime? EndDateFrom,
        [FromQuery] DateTime? EndDateTo,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10)
    {
        var request = new GetAllInteractionRequest(
            Search: search,
            TeamMemberId: TeamMemberId,
            ContactId: ContactId,
            StartDateFrom: StartDateFrom,
            StartDateTo: StartDateTo,
            EndDateFrom: EndDateFrom,
            EndDateTo: EndDateTo,
            Page: page,
            Limit: limit
        );
        var res = await _mediator.Send(new GetAllInteractionQuery(request));
        return Ok(res);
    }

    [HasAuthorization(PermModule.Interaction, PermAction.Delete)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteInteractionResponse>>> DeleteCompany   
        (Guid Id, [FromQuery] DeleteInteractionRequest request)
    {
        var res = await _mediator.Send(new DeleteInteractionCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.Report, PermAction.Create)]
    [HttpPost("{Id}/report")]
    public async Task<ActionResult<ApiResponse<CreateReportResponse>>> CreateReport(
        Guid Id,
        [FromForm] CreateReportRequest body)
    {
        var res = await _mediator.Send(new CreateReportCommand
        {
            InteractionId = Id,
            Name = body.Name,
            Description = body.Description,
            ActionPlan = body.ActionPlan,
            StartDate = body.Date.StartDate,
            EndDate = body.Date.EndDate,
            Contacts = body.Present.Contacts,
            TeamMembers = body.Present.TeamMembers,
            Attachments = body.Attachments
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.Report, PermAction.Read)]
    [HttpGet("{Id}/report")]
    public async Task<ActionResult<ApiResponse<GetReportResponse>>> GetReport(
        Guid Id)
    {
        var res = await _mediator.Send(
            new GetReportQuery(
                new GetReportRequest(Id)));
        return Ok(res);
    }
}