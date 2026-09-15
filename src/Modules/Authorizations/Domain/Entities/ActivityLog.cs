using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Authorizations.Domain.Entities
{
    public class ActivityLog
        : BaseEntity, ITenantScoped
    {
        public string EventType { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid AuthorId { get; set; }
        public string PayloadJson { get; set; } = "{}";
        public DateTime OccuredAt { get; set; } = DateTime.UtcNow;

        private ActivityLog(){}
        private ActivityLog(Guid tenantId, Guid companyId, string eventType, Guid authorId, string payloadJson)
        {
            TenantId = tenantId;
            CompanyId = companyId;
            EventType = eventType;
            AuthorId = authorId;
            PayloadJson = payloadJson;
        }

        public static ActivityLog Create(Guid tenantId, Guid companyId, string eventType, Guid authorId, string payloadJson)
            => new(tenantId, companyId, eventType, authorId, payloadJson);
    }
}