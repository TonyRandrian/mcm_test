using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Property.Domain.Events
{
    public record PropertyCreatedEvent
    (
        Guid PropertyId,
        string Name
    ) : IDomainEvent;
}