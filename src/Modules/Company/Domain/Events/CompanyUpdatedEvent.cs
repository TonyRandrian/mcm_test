using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Events
{
    public record CompanyUpdatedEvent(
        Guid CompanyId,
        string Name
    ) : IDomainEvent;
}