using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Events
{
    public record TypeContactCreatedEvent(
        Guid TypeContactId,
        string Name
    ) : IDomainEvent;
}