using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record ReportCreatedEvent
    (
        Guid InteractionId, 
        string Name
    ) : IDomainEvent;
}