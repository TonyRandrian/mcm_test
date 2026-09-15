using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mcm.Authorizations.Infrastructure.Repositories
{
    public class RoleRepository(AuthorizationDbContext context)
        : GenericRepository<Role>(context), IRoleRepository
    {
        public async Task<Role?> GetAdminAsync()
        {
            IQueryable<Role> query = _dbSet
                .Include(r => r.RoleCompanies)
                .Where(r => r.Title.Value.Equals("Administrateur") && r.IsSystem);
            return await query
                .FirstOrDefaultAsync();
        }

        public override Task<Role?> GetByIdAsync(Guid id)
        {
            IQueryable<Role> query = _dbSet
                .Include(r => r.Members)
                .Include(r => r.Authorizations)
                .Include(r => r.RoleCompanies);
            return query.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}