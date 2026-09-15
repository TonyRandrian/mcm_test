using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ServiceCategoryUpdatedEvent
    (
        Guid ServiceCategoryId,
        string Name
    ) : IDomainEvent;
}