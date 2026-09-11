using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InvitationAcceptedTeamMember
{
    public record InvitationAcceptedTeamMemberRequest
    (
        string FirstName,
        string LastName,
        string Email,
        string AccessToken,
        string Password
    );
}