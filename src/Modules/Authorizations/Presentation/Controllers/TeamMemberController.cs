using Mcm.Authorizations.Application.Features.Password.ResetPassword;
using Mcm.Authorizations.Application.Features.TeamMembers.Commands.InvitationAcceptedTeamMember;
using Mcm.Authorizations.Application.Features.TeamMembers.Commands.InvitationDeniedTeamMember;
using Mcm.Authorizations.Application.Features.TeamMembers.Commands.InviteTeamMember;
using Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdateInfoTeamMember;
using Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdateTmValue;
using Mcm.Authorizations.Application.Features.TeamMembers.Commands.UploadPhoto;
using Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetAllTeamMember;
using Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetCurrentTeamMember;
using Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetTeamMember;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Authorizations.Presentation.Controllers;

[ApiController]
[Route("api/teamMember")]
public class TeamMemberController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("me")]
    public async Task<ActionResult<ApiResponse<GetCurrentTeamMemberResponse>>> CurrentUser()
    {
        var res = await _mediator.Send(new GetCurrentTeamMemberQuery());
        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllTeamMemberResponse>>> GetAll
        ([FromQuery] GetAllTeamMemberRequestHeader header)
    {
        var res = await _mediator.Send(new GetAllTeamMemberQuery(header));
        return Ok(res);
    }

    [HasAuthorization(PermModule.TeamMember, PermAction.Read)]
    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetTeamMemberResponse>>> GetById
        (Guid Id)
    {
        var res = await _mediator.Send(new GetTeamMemberQuery(
            new GetTeamMemberRequest(Id)
        ));
        return Ok(res);
    }

    [HasAuthorization(PermModule.TeamMember, PermAction.Update)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateInfoTeamMemberResponse>>> Update
        (Guid Id, [FromBody] UpdateInfoTeamMemberRequest body)
    {
        var res = await _mediator.Send(new UpdateInfoTeamMemberCommand
        {
            Id = Id,
            LastName = body.LastName,
            FirstName = body.FirstName,
            Position = body.Position,
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.TeamMember, PermAction.Update)]
    [HttpPost("{Id}/photo")]
    public async Task<ActionResult<ApiResponse<UploadPhotoResponse>>> UploadPhoto
        (Guid Id, [FromForm] UploadPhotoRequest body)
    {
        var res = await _mediator.Send(new UploadPhotoCommand
        {
            Id = Id,
            Image = body.Image
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.TeamMember, PermAction.Update)]
    [HttpPost("{Id}/reset-password")]
    public async Task<ActionResult<ApiResponse<ResetPasswordResponse>>> ResetPassword
        (Guid Id, [FromForm] ResetPasswordRequest body)
    {
        var res = await _mediator.Send(new ResetPasswordCommand
        {
            Id = Id,
            Password = body.Password
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.TeamMember, PermAction.Create)]
    [HttpPost("/api/company/{Id}/invite")]
    public async Task<ActionResult<ApiResponse<InviteTeamMemberResponse>>> Invite
        (Guid Id, [FromBody] InviteTeamMemberRequestBody body)
    {
        var res = await _mediator.Send(new InviteTeamMemberCommand
        {
            CompanyId = Id,
            FirstName = body.FirstName,
            LastName = body.LastName,
            Email = body.Email,
            RoleId = body.RoleId,
            IsLeader = body.IsLeader
        });
        return Ok(res);
    }

    [AllowAnonymous]
    [HttpPost("invitation_accepted")]
    public async Task<ActionResult<ApiResponse<InvitationAcceptedTeamMemberResponse>>> AcceptedInvitation
        ([FromBody] InvitationAcceptedTeamMemberRequest body)
    {
        var res = await _mediator.Send(new InvitationAcceptedTeamMemberCommand
        {
            FirstName = body.FirstName,
            LastName = body.LastName,
            AccessToken = body.AccessToken,
            Password = body.Password,
        });
        return Ok(res);
    }

    [AllowAnonymous]
    [HttpPost("invitation_denied")]
    public async Task<ActionResult<ApiResponse<InvitationDeniedTeamMemberResponse>>> DeniedInvitation
        ([FromBody] InvitationDeniedTeamMemberRequest body)
    {
        var res = await _mediator.Send(new InvitationDeniedTeamMemberCommand
        {
            AccessToken = body.AccessToken,
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.TeamMember, PermAction.Update)]
    [HttpPut("{Id}/values")]
    public async Task<ActionResult<ApiResponse<UpdateTmValueResponse>>> UpdateTmValue(
        Guid Id, [FromBody] List<UpdateTmValueRequest> body)
    {
        var res = await _mediator.Send(new UpdateTmValueCommand
        {
            TeamMemberId = Id,
            Values = body,
        });
        return Ok(res);
    }
}
