using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Shared.Domain.Events
{
    public record LeaderDefinedEvent
    (
        Guid TeamMemberId,
        Guid CompanyId,
        string CompanyName
    ) : IDomainEvent;
}