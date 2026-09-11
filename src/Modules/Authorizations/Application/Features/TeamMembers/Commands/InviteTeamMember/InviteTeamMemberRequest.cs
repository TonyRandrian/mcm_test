using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InviteTeamMember
{
    public record InviteTeamMemberRequestBody
    (
        string? FirstName,
        string? LastName,
        string Email,
        Guid? RoleId,
        bool IsLeader
    );
}