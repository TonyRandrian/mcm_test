using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ServiceCategoryCreatedEvent
    (
        Guid ServiceCategoryId,
        string Name
    ) : IDomainEvent;
}