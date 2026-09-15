using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Contacts.Domain.Events
{
    public record class ContactUpdatedEvent
    (
        Guid ContactId,
        string FullName
    ) : IDomainEvent;
}