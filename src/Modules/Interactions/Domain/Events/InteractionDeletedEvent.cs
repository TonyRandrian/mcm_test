using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record InteractionDeletedEvent
    (
        Guid InteractionId, 
        string Title
    ) : IDomainEvent;
}