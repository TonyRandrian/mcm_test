namespace Mcm.Authorizations.Application.Features.Roles.Queries.GetRole
{
    public class GetRoleResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<(string Module, string Action)> Permissions { get; set; } = [];
        public IEnumerable<(Guid Id, string Name)> Companies { get; set; } = [];
    }
}