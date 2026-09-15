using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Events
{
    public record ServiceDeletedEvent
    (
        Guid ServiceId,
        string Name
    ) : IDomainEvent;
}