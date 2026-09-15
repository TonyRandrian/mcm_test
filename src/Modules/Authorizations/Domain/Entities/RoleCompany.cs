using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Authorizations.Domain.Entities
{
    public class RoleCompany
        : ITenantScoped
    {
        public Guid RoleId { get; private set; }
        public Guid CompanyId { get; private set; }
        public Guid TenantId { get; set; }
        
        private RoleCompany(){}
        private RoleCompany(Guid roleId, Guid companyId, Guid tenantId)
        {
            RoleId = roleId;
            CompanyId = companyId;
            TenantId = tenantId;
        }

        public static RoleCompany Create(Guid roleId, Guid companyId, Guid tenantId)
            => new(roleId, companyId, tenantId);
            
    }
}