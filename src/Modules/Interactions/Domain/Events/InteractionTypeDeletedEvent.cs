using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record InteractionTypeDeletedEvent
    (
        Guid InteractionTypeId, 
        string Title
    ) : IDomainEvent;
}