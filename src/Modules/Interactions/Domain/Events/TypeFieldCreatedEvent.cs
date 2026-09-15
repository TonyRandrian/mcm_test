using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record TypeFieldCreatedEvent
    (
        Guid TypeFieldId, 
        string Name
    ) : IDomainEvent;
}