using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Entities
{
    public class ReportContact
        : ITenantScoped
    {
        public Guid ReportId { get; private set; }
        public Guid ContactId { get; private set; }
        public Guid TenantId { get; set; }
        public Report Report { get; private set; } = null!;

        private ReportContact() {}
        private ReportContact(Guid reportId, Guid contactId)
        {
            ReportId = reportId;
            ContactId = contactId;
        }

        public static ReportContact Create(Guid reportId, Guid contactId)
            => new(reportId, contactId);
    }
}