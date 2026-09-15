using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record InteractionTypeCreatedEvent
    (
        Guid InteractionTypeId, 
        string Title
    ) : IDomainEvent;
}