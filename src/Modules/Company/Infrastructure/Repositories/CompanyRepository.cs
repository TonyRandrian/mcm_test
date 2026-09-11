using Mcm.Company.Application.Interfaces;
using Mcm.Company.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Company.Infrastructure.Repositories
{
    public class CompanyRepository(CompanyDbContext context)
        : GenericRepository<Domain.Entities.Company>(context), ICompanyRepository
    {
        public Task<Domain.Entities.Company?> FindCompanyByIdOutTenant(Guid id)
        {
            return _dbSet.IgnoreQueryFilters(["MultiTenant"])
                .Include(c => c.SupplValues)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}