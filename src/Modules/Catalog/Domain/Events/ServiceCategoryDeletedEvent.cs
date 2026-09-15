using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ServiceCategoryDeletedEvent
    (
        Guid ServiceCategoryId,
        string Name
    ) : IDomainEvent;
}