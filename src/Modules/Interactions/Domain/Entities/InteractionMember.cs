using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Entities
{
    public class InteractionMember : ITenantScoped
    {
        public Guid InteractionId { get; private set; }
        public Guid TeamMemberId { get; private set; }
        public Guid TenantId { get; set; }
        public Interaction Interaction { get; private set; } = null!;


        private InteractionMember() {}
        private InteractionMember(Guid interactionId, Guid teamMemberId)
        {
            InteractionId = interactionId;
            TeamMemberId = teamMemberId;
        }

        public static InteractionMember Create(Guid interactionId, Guid teamMemberId)
            => new(interactionId, teamMemberId);
    }
}