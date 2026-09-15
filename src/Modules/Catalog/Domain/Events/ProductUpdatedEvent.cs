using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ProductUpdatedEvent
    (
        Guid ProductId,
        string Name
    ) : IDomainEvent;
}