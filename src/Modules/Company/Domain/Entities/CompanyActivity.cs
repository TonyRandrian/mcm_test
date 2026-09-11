using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Entities
{
    public class CompanyActivity : ITenantScoped 
    {
        public Guid CompanyId { get; private set; }
        public Guid ActivitySectorId { get; private set; }
        public Company Company { get; private set; } = null!;
        public ActivitySector ActivitySector { get; private set; } = null!;
        public Guid TenantId { get; set; }

        private CompanyActivity() { }
        private CompanyActivity(Guid companyId, Guid activitySectorId, Guid tenantId)
        {
            CompanyId = companyId;
            ActivitySectorId = activitySectorId;
            TenantId = tenantId;
        }

        public static CompanyActivity Create(Guid companyId, Guid activitySectorId, Guid tenantId)
            => new(companyId, activitySectorId, tenantId);
    }
}