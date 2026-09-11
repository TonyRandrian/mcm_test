using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Entities
{
    public class InteractionContact : ITenantScoped
    {
        public Guid InteractionId { get; private set; }
        public Guid ContactId { get; private set; }
        public Guid TenantId { get; set; }
        public Interaction Interaction { get; private set; } = null!;


        private InteractionContact() {}
        private InteractionContact(Guid interactionId, Guid contactId)
        {
            InteractionId = interactionId;
            ContactId = contactId;
        }

        public static InteractionContact Create(Guid interactionId, Guid contactId)
            => new(interactionId, contactId);
    }
}