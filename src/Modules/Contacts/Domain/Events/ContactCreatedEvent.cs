using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Contacts.Domain.Events
{
    public record class ContactCreatedEvent
    (
        Guid ContactId,
        string FullName
    ) : IDomainEvent;
}