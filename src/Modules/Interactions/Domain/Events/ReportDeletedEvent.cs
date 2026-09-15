using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Interactions.Domain.Events
{
    public record ReportDeletedEvent
    (
        Guid InteractionId, 
        string Name
    ) : IDomainEvent;
}