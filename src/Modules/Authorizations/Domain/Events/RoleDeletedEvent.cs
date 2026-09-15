using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Authorizations.Domain.Events
{
    public record RoleDeletedEvent
    (
        Guid RoleId,
        string Title
    ) : IDomainEvent;
}