using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ProductCategoryDeletedEvent
    (
        Guid ProductCategoryId,
        string Name
    ) : IDomainEvent;
}