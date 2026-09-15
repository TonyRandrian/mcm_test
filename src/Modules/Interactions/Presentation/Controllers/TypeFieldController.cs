using Mcm.Interactions.Application.Features.TypeFields.Commands.DeleteTypeField;
using Mcm.Interactions.Application.Features.TypeFields.Commands.UpdateTypeField;
using Mcm.Interactions.Application.Features.TypeFields.Queries.GetAllTypeField;
using Mcm.Interactions.Application.Features.TypeFields.Queries.GetTypeField;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Interactions.Presentation.Controllers;

[ApiController]
[Route("api/typeFields")]
public class TypeFieldController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HasAuthorization(PermModule.InteractionType, PermAction.Read)]
    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetTypeFieldResponse>>> GetById(        
        Guid Id)
    {
        var res = await _mediator.Send(new GetTypeFieldQuery(
            new GetTypeFieldRequest(Id)
        ));
        return Ok(res);
    }

    [HasAuthorization(PermModule.InteractionType, PermAction.Update)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateTypeFieldResponse>>> Update(
        Guid Id, [FromForm] UpdateTypeFieldRequest body)
    {
        var res = await _mediator.Send(new UpdateTypeFieldCommand
        {
            TypeFieldId = Id,
            Name = body.Name,
            Type = body.Type
        });
        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllTypeFieldResponse>>> GetAll(
        [FromQuery] string? search = null,
        [FromQuery] Guid? interactionTypeId = null,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 5)
    {
        var request = new GetAllTypeFieldRequest(
            Search: search,
            InteractionTypeId: interactionTypeId,
            Page: page,
            Limit: limit
        );
        var res = await _mediator.Send(new GetAllTypeFieldQuery(request));
        return Ok(res);
    }

    [HasAuthorization(PermModule.InteractionType, PermAction.Delete)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteTypeFieldResponse>>> DeleteCompany   
        (Guid Id, [FromQuery] DeleteTypeFieldRequest request)
    {
        var res = await _mediator.Send(new DeleteTypeFieldCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(res);
    }

}