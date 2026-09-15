using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record InteractionCreatedEvent(Guid InteractionId, string Title, DataTime Date, Reminder Reminder)
        : IDomainEvent;
}