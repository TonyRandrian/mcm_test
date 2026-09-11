using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Authorizations.Infrastructure.Repositories
{
    public class TeamMemberRepository(AuthorizationDbContext context)
        : GenericRepository<TeamMember>(context), ITeamMemberRepository
    {
        public async Task<TeamMember?> GetByEmailAsync(Email email)
        {
            return await _dbSet
                    .IgnoreQueryFilters(["MultiTenant"])
                    // .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Identity.Email.Equals(email));
        }

        public async Task<TeamMember?> GetByIdOutTenantAsync(Guid id)
        {
            return await _dbSet
                    .IgnoreQueryFilters(["MultiTenant"])
                    // .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}   