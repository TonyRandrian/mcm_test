using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Features.Roles.Queries.GetAllRole
{
    public class GetAllRoleResponse
    {
        public List<RoleResponse> Roles { get; set; } = [];
    }

    public class RoleResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Count { get; set; }
        
    }
}