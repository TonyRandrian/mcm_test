using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Shared.Application.Events
{
    public record AdminRegisteredEvent
    (
        Guid TeamMemberId,
        Guid CompanyId
    ) : IDomainEvent;
}