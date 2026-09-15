using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Events
{
    public record SubsidiaryUpdatedEvent(
        Guid CompanyId,
        string Name
    ) : IDomainEvent;
}