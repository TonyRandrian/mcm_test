namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetAllTeamMember
{
    public record GetAllTeamMemberRequestHeader
    (
        int Page = 1,
        int Limit = 10
    );
}