using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Interfaces
{
    public interface ITeamMemberRepository
        : IGenericRepository<TeamMember>
    {
        Task<TeamMember?> GetByEmailAsync(Email email);
        Task<TeamMember?> GetByIdOutTenantAsync(Guid id);
        bool IsAuthorized(Guid teamMemberId, Guid companyId);
    }
}