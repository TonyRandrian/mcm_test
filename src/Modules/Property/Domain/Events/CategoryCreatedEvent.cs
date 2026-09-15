using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Property.Domain.Events
{
    public record CategoryCreatedEvent
    (
        Guid CategoryId,
        string Name
    ) : IDomainEvent;
}