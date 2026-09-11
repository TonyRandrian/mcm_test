using Mcm.Property.Application.Features.Properties.Commands.DeleteProperty;
using Mcm.Property.Application.Features.Properties.Commands.UpdateProperty;
using Mcm.Property.Application.Features.Properties.Queries.GetProperty;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Property.Presentation.Controllers;

[ApiController]
[Route("api/property")]
public class PropertyController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetPropertyResponse>>> GetById(
        Guid Id)
    {
        var result = await _mediator.Send(new GetPropertyQuery(new GetPropertyRequest(Id)));
        return Ok(result);
    }

    // [HasAuthorization(PermissionsEnum.Update_Property)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdatePropertyResponse>>> Update(
        Guid Id, [FromBody] UpdatePropertyRequest body)
    {
        var result = await _mediator.Send(new UpdatePropertyCommand{
            Id = Id,
            Name = body.Name,
            Description = body.Description,
            Type = body.Type,
        });
        return Ok(result);
    }

    // [HasAuthorization(PermissionsEnum.Delete_Property)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeletePropertyResponse>>> Delete(
        Guid Id, [FromQuery] DeletePropertyRequest request)
    {
        var result = await _mediator.Send(new DeletePropertyCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(result);
    }
}