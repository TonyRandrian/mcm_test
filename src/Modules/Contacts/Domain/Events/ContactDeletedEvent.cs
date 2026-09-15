using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Contacts.Domain.Events
{
    public record class ContactDeletedEvent
    (
        Guid ContactId,
        string FullName
    ) : IDomainEvent;
}