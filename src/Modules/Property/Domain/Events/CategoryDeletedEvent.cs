using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Property.Domain.Events
{
    public record CategoryDeletedEvent
    (
        Guid CategoryId,
        string Name
    ) : IDomainEvent;
}