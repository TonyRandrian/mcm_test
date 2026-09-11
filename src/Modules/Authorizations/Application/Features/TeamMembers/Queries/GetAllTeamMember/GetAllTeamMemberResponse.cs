using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetAllTeamMember
{
    public class GetAllTeamMemberResponse
    {
        public List<TeamMemberResponse> TeamMembers { get; set; } = [];
    }

    public class TeamMemberResponse
    {
        public Guid Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}