using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Shared.Domain.Events
{
    public record CompanyContactCreatedEvent(
        Guid CompanyId,
        string Name
    ) : IDomainEvent;
}