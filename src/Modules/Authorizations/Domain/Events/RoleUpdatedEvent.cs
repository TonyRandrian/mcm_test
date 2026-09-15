using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Authorizations.Domain.Events
{
    public record RoleUpdatedEvent
    (
        Guid RoleId,
        string Title
    ) : IDomainEvent;
}