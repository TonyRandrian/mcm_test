using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ProductDeletedEvent
    (
        Guid ProductId,
        string Name
    ) : IDomainEvent;
}