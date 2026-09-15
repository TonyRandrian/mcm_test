using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Property.Domain.Events
{
    public record PropertyUpdatedEvent
    (
        Guid PropertyId,
        string Name
    ) : IDomainEvent;
}