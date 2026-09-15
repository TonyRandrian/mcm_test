using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Events
{
    public record TypeContactUpdatedEvent(
        Guid TypeContactId,
        string Name
    ) : IDomainEvent;
}