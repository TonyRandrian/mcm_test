using MediatR;

namespace Mcm.Shared.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid TenantId { get; }
        Guid TeamMemberId { get; }
        Guid CompanyId { get; }
    }
}