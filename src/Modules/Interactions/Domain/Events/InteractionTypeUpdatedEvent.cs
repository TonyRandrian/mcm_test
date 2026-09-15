using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record InteractionTypeUpdatedEvent
    (
        Guid InteractionTypeId, 
        string Title
    ) : IDomainEvent;
}