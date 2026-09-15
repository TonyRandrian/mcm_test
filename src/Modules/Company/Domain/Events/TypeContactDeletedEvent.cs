using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Events
{
    public record TypeContactDeletedEvent(
        Guid TypeContactId,
        string Name
    ) : IDomainEvent;
}