using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ServiceCreatedEvent
    (
        Guid ServiceId,
        string Name
    ) : IDomainEvent;
}