using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ProductCreatedEvent
    (
        Guid ProductId,
        string Name
    ) : IDomainEvent;
}