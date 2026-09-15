using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Shared.Domain.Events
{
    public record CompanyDeletedEvent
    (
        Guid CompanyId,
        string Name
    )  : IDomainEvent;
}