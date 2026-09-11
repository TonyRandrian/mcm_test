using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Authorizations.Domain.Entities
{
    public class RoleTeamMember : ITenantScoped
    {
        public Guid RoleId { get; set; }
        public Guid TeamMemberId { get; set; }

        public Role Role { get; set; } = null!;
        public TeamMember TeamMember { get; set; } = null!;
        public Guid TenantId { get; set; }

        private RoleTeamMember(){}
        private RoleTeamMember(Guid roleId, Guid tmId, Guid tenantId)
        {
            RoleId = roleId;
            TeamMemberId = tmId;
            TenantId = tenantId;
        }

        public static RoleTeamMember Create(Guid roleId, Guid tmId, Guid tenantId)
            => new(roleId, tmId, tenantId);
    }
}