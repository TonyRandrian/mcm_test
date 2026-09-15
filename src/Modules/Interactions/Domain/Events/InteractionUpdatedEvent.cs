using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record InteractionUpdatedEvent
    (
        Guid InteractionId, 
        string Title
    ) : IDomainEvent;
}