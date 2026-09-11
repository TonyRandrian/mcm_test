using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Domain.Events
{
    public record TeamMemberInvited
    (
        Guid Id,
        Identity Identity,
        Guid CompanyId,
        string Token
    ) : IDomainEvent;

}