using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Authorizations.Domain.Events
{
    public record RoleCreatedEvent
    (
        Guid RoleId,
        string Title
    ) : IDomainEvent;
}