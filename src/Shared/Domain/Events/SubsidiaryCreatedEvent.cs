using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Shared.Domain.Events
{
    public record SubsidiaryCreatedEvent
    (
        Guid CompanyId,
        string Name
    )
        : IDomainEvent;
}