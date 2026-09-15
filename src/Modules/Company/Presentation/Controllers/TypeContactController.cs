using Mcm.Company.Application.Features.TypeContacts.Commands.CreateTypeContact;
using Mcm.Company.Application.Features.TypeContacts.Commands.DeleteTypeContact;
using Mcm.Company.Application.Features.TypeContacts.Commands.UpdateTypeContact;
using Mcm.Company.Application.Features.TypeContacts.Queries.GetAllTypeContact;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Company.Presentation.Controllers;

[ApiController]
[Route("api/company/typeContact")]
public class TypeContactController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HasAuthorization(PermModule.TypeContact, PermAction.Create)]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateTypeContactResponse>>> Create
        ([FromBody] CreateTypeContactRequest body)
    {
        var res = await _mediator.Send(new CreateTypeContactCommand
        {
            Name = body.Name,
            Description = body.Description,
            Color = body.Color,
            TypeConvertTo = body.TypeConvertTo
        });
        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllTypeContactRequest>>> GetAll
        ([FromQuery] GetAllTypeContactRequest request)
    {
        var res = await _mediator.Send(new GetAllTypeContactQuery(request));
        return Ok(res);
    }

    [HasAuthorization(PermModule.TypeContact, PermAction.Update)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateTypeContactResponse>>> Update(
        Guid Id, [FromForm] UpdateTypeContactRequest body)
    {
        var res = await _mediator.Send(new UpdateTypeContactCommand
        {
            Id = Id,
            Name = body.Name,
            Color = body.Color,
            Description = body.Description,
        });
        return Ok(res);
    }


    [HasAuthorization(PermModule.TypeContact, PermAction.Delete)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteTypeContactResponse>>> DeleteTypeContact   
        (Guid Id, [FromQuery] DeleteTypeContactRequest request)
    {
        var res = await _mediator.Send(new DeleteTypeContactCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(res);
    }
}