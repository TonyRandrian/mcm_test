using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ServiceUpdatedEvent
    (
        Guid ServiceId,
        string Name
    ) : IDomainEvent;
}