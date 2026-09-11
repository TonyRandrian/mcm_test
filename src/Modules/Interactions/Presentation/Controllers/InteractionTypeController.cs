using System.Text.Json;
using Mcm.Interactions.Application.Features.InteractionTypes.Commands.CreateInteractionType;
using Mcm.Interactions.Application.Features.InteractionTypes.Commands.DeleteInteractionType;
using Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType;
using Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType;
using Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetInteractionType;
using Mcm.Interactions.Application.Features.TypeFields.Commands.CreateTypeField;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Interactions.Presentation.Controllers;

[ApiController]
[Route("api/interactionTypes")]
public class InteractionTypeController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateInteractionTypeResponse>>> Create(
        [FromBody] CreateInteractionTypeRequest body)
    {
        var res = await _mediator.Send(new CreateInteractionTypeCommand
        {
            Title = body.Title,
            Description = body.Description,
            LabelColor = body.LabelColor,
            ParentId = body.ParentId
        });
        return Ok(res);
    }

    // [HasAuthorization(PermissionsEnum.View_Company)] 
    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetInteractionTypeResponse>>> GetById(        
        Guid Id)
    {
        var res = await _mediator.Send(new GetInteractionTypeQuery(
            new GetInteractionTypeRequest(Id)
        ));
        return Ok(res);
    }

    // [HasAuthorization(PermissionsEnum.Update_Category)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateInteractionTypeResponse>>> Update(
        Guid Id, [FromBody] UpdateInteractionTypeRequest body)
    {
        var res = await _mediator.Send(new UpdateInteractionTypeCommand
        {
            Id = Id,
            Title = body.Title,
            Description = body.Description,
            LabelColor = body.LabelColor
        });
        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllInteractionTypeResponse>>> GetAll(
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 5)
    {
        var request = new GetAllInteractionTypeRequest(
            Search: search,
            Page: page,
            Limit: limit
        );
        var res = await _mediator.Send(new GetAllInteractionTypeQuery(request));
        return Ok(res);
    }

    // [HasAuthorization(PermissionsEnum.Delete_Company)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteInteractionTypeResponse>>> DeleteCompany   
        (Guid Id, [FromQuery] DeleteInteractionTypeRequest request)
    {
        var res = await _mediator.Send(new DeleteInteractionTypeCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(res);
    }

    [HttpPost("{Id}/fields")]
    public async Task<ActionResult<ApiResponse<CreateTypeFieldResponse>>> AddField(
        Guid Id,
        [FromForm] CreateTypeFieldRequest body)
    {
        var res = await _mediator.Send(new CreateTypeFieldCommand
        {
            Name = body.Name,
            Type = body.Type,
            InteractionTypeId = Id
        });
        return Ok(res);
    }

}