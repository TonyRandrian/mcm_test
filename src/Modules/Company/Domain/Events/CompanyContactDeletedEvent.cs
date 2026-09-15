using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Events
{
    public record CompanyContactDeletedEvent(
        Guid CompanyId,
        string Name
    ) : IDomainEvent;
}