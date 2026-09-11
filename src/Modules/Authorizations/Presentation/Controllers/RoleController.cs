using Mcm.Authorizations.Application.Features.Roles.Commands.CreateRole;
using Mcm.Authorizations.Application.Features.Roles.Commands.DeleteRole;
using Mcm.Authorizations.Application.Features.Roles.Commands.UpdateRole;
using Mcm.Authorizations.Application.Features.Roles.Queries.GetAllRole;
using Mcm.Authorizations.Application.Features.Roles.Queries.GetRole;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Company.Presentation.Controllers;

[ApiController]
[Route("api/role")]
public class RoleController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // [HasAuthorization(PermissionsEnum.Create_Role)]
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

    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetRoleResponse>>> GetById
        (Guid Id)
    {
        var res = await _mediator.Send(new GetRoleQuery(
            new GetRoleRequest(Id)
        ));
        return Ok(res);
    }

    // [HasAuthorization(PermissionsEnum.Update_Role)]
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

    // [HasAuthorization(PermissionsEnum.Delete_Role)]
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