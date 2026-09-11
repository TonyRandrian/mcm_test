using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdateInfoTeamMember
{
    public record UpdateInfoTeamMemberRequest
    (
        string FirstName,
        string LastName,
        string? Position
    );
}