using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record TypeFieldDeletedEvent
    (
        Guid TypeFieldId, 
        string Name
    ) : IDomainEvent;
}