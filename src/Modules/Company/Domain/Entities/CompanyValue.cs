using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Company.Domain.Entities
{
    public class CompanyValue
    {
        public Guid Id { get; private set; }
        public string Data { get; set; } = string.Empty;
        public Guid PropertyId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid TenantId { get; set; }

        private CompanyValue() {}
        private CompanyValue(Guid companyId, Guid propertyId, string data, Guid tenantId)
        {
            CompanyId = companyId;
            PropertyId = propertyId;
            Data = data;
            TenantId = tenantId;
        }
        public static CompanyValue Create(Guid companyId, Guid propertyId, string data, Guid tenantId)
            => new(companyId, propertyId, data, tenantId);

        
    }
}