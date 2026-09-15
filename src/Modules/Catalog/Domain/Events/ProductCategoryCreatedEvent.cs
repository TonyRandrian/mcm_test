using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ProductCategoryCreatedEvent
    (
        Guid ProductCategoryId,
        string Name
    ) : IDomainEvent;
}