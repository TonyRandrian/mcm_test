using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ProductCategoryUpdatedEvent
    (
        Guid ProductCategoryId,
        string Name
    ) : IDomainEvent;
}