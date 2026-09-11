using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InvitationDeniedTeamMember
{
    public record InvitationDeniedTeamMemberRequest
    (
        string AccessToken
    );
}