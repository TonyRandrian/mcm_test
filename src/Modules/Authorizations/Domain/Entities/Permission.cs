using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Authorizations.Domain.Entities
{
    public class Permission : BaseEntity, ITenantScoped
    {
        public PermModule Module { get; private set; }
        public PermAction Action { get; private set; }
        public Guid RoleId { get; private set; }

        public Role Role { get; private set; } = null!;
        public Guid TenantId { get; set; }

        private Permission(){}
        private Permission(Guid roleId, PermModule module, PermAction action, Guid tenantId)
        {
            RoleId = roleId;
            Module = module;
            Action = action;
            TenantId = tenantId;
        }

        public static Permission Create(Guid roleId, PermModule module, PermAction action, Guid tenantId)
        {
            return new Permission(roleId, module, action, tenantId);
        }
    }
}