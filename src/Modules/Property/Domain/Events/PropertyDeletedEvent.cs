using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Property.Domain.Events
{
    public record PropertyDeletedEvent
    (
        Guid PropertyId,
        string Name
    ) : IDomainEvent;
}