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
        public override Task<TeamMember?> GetByIdAsync(Guid id)
        {
            IQueryable<TeamMember> query = _dbSet
                .Include(tm => tm.Roles)
                .ThenInclude(tr => tr.Role);
            return query.FirstOrDefaultAsync(tm => tm.Id == id);
        }

        public async Task<TeamMember?> GetByEmailAsync(Email email)
        {
            return await _dbSet
                    .IgnoreQueryFilters(["MultiTenant"])
                    .FirstOrDefaultAsync(t => t.Identity.Email.Equals(email));
        }

        public async Task<TeamMember?> GetByIdOutTenantAsync(Guid id)
        {
            return await _dbSet
                    .IgnoreQueryFilters(["MultiTenant"])
                    .FirstOrDefaultAsync(t => t.Id == id);
        }

        public bool IsAuthorized(Guid teamMemberId, Guid companyId)
        {
            var roles = _dbSet
                .Where(tm => tm.Id == teamMemberId)
                .SelectMany(tm => tm.Roles)
                .Select(tm => tm.RoleId);
            return _context.Set<RoleCompany>()
                .Any(rc => roles.Contains(rc.RoleId) && rc.CompanyId == companyId);
        }
    }
}   