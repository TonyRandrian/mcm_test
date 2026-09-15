namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetCurrentTeamMember
{
    public class GetCurrentTeamMemberResponse
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public List<GetCurrentTeamMember_Permission> Permissions { get; set; } = [];
        public List<GetCurrentTeamMember_Company> Companies { get; set; } = [];
        public List<TeamMemberCategoryDataResponse> SupplementaryData { get; set; } = [];
    }

    public record GetCurrentTeamMember_Permission(string Module, HashSet<string> Action);
    public record GetCurrentTeamMember_Company(Guid Id, string Name, string Acronym);

    public class TeamMemberCategoryDataResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<TeamMemberPropertyData> Informations { get; set; } = [];
    }
    public class TeamMemberPropertyData
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public bool IsSensitive { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}