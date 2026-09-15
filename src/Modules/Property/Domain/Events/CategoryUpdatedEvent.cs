using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Property.Domain.Events
{
    public record CategoryUpdatedEvent
    (
        Guid CategoryId,
        string Name
    ) : IDomainEvent;
}