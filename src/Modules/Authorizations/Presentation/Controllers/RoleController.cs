using Mcm.Authorizations.Application.Features.Roles.Commands.CreateRole;
using Mcm.Authorizations.Application.Features.Roles.Commands.DeleteRole;
using Mcm.Authorizations.Application.Features.Roles.Commands.UpdateRole;
using Mcm.Authorizations.Application.Features.Roles.Queries.GetAllRole;
using Mcm.Authorizations.Application.Features.Roles.Queries.GetRole;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Authorizations.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/role")]
public class RoleController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HasAuthorization(PermModule.Role, PermAction.Create)]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateRoleResponse>>> Create(
        [FromBody] CreateRoleRequest body)
    {
        var res = await _mediator.Send(new CreateRoleCommand
        {
            Title = body.Title,
            Description = body.Description,
            Permissions = body.Permissions,
            Companies = body.Companies,
        });
        return Ok(res);
    }  

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllRoleResponse>>> GetAll(
        [FromQuery] GetAllRoleRequest query)
    {
        var res = await _mediator.Send(
            new GetAllRoleQuery(query));
        return Ok(res);
    }

    [HasAuthorization(PermModule.Role, PermAction.Read)]
    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetRoleResponse>>> GetById
        (Guid Id)
    {
        var res = await _mediator.Send(new GetRoleQuery(
            new GetRoleRequest(Id)
        ));
        return Ok(res);
    }

    [HasAuthorization(PermModule.Role, PermAction.Update)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateRoleResponse>>> Update
        (Guid Id, [FromBody] UpdateRoleRequest body)
    {
        var res = await _mediator.Send(new UpdateRoleCommand
        {
            Id = Id,
            Title = body.Title,
            Description = body.Description,
            Permissions = body.Permissions,
            Companies = body.Companies,
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.Role, PermAction.Delete)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteRoleResponse>>> Delete(
        Guid Id, [FromQuery] DeleteRoleRequest request)
    {
        var res = await _mediator.Send(new DeleteRoleCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(res);
    }
}