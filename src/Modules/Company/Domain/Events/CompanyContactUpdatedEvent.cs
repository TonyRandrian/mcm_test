using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Company.Domain.Events
{
    public record CompanyContactUpdatedEvent(
        Guid CompanyId,
        string Name
    ) : IDomainEvent;
}