using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Authorizations.Domain.Entities
{
    public class TeamMemberValue
    {
        public Guid Id { get; private set; }
        public Guid TeamMemberId { get; private set; }
        public Guid TenantId { get; set; }
        public Guid PropertyId { get; private set; }
        public string Data { get;  set; } = string.Empty;

        private TeamMemberValue() {}
        private TeamMemberValue(Guid teamMemberId, Guid propertyId, string data, Guid tenantId)
        {
            TeamMemberId = teamMemberId;
            PropertyId = propertyId;
            Data = data;
            TenantId = tenantId;
        }
        public static TeamMemberValue Create(Guid teamMemberId, Guid propertyId, string data, Guid tenantId)
            => new(teamMemberId, propertyId, data, tenantId);
    }
}