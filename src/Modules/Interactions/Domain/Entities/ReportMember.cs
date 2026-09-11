using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Entities
{
    public class ReportMember
        : ITenantScoped
    {
        public Guid ReportId { get; private set; }
        public Guid TeamMemberId { get; private set; }
        public Guid TenantId { get; set; }
        public Report Report { get; private set; } = null!;

        private ReportMember() {}
        private ReportMember(Guid reportId, Guid teamMemberId)
        {
            ReportId = reportId;
            TeamMemberId = teamMemberId;
        }

        public static ReportMember Create(Guid reportId, Guid teamMemberId)
            => new(reportId, teamMemberId);
    }
}