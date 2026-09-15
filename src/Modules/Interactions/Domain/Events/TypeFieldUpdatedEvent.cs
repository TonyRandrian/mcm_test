using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record TypeFieldUpdatedEvent
    (
        Guid TypeFieldId, 
        string Name
    ) : IDomainEvent;
}