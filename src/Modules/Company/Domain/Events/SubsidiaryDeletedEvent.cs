using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Events
{
    public record SubsidiaryDeletedEvent(
        Guid CompanyId,
        string Name
    ) : IDomainEvent;
}