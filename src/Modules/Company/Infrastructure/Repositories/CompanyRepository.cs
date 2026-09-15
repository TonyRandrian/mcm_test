using Mcm.Company.Application.Features.Dashboard;
using Mcm.Company.Application.Interfaces;
using Mcm.Company.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Company.Infrastructure.Repositories
{
    public class CompanyRepository(CompanyDbContext context)
        : GenericRepository<Domain.Entities.Company>(context), ICompanyRepository
    {
        public override Task<Domain.Entities.Company?> GetByIdAsync(Guid id)
        {
            IQueryable<Domain.Entities.Company> query = _dbSet
                .Include(c => c.CompanyActivities)
                .Include(c => c.TypeContact)
                .Include(c => c.Parent);
            return query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Domain.Entities.Company?> FindCompanyByIdOutTenant(Guid id)
        {
            return _dbSet.IgnoreQueryFilters(["MultiTenant"])
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<TypeContactViewData>> GetDashboardData(Guid companyId)
        {
            var companies = _dbSet.AsNoTracking()
                .Where(c => c.IsContact && c.TypeContactId != Guid.Empty && c.CompanyId == companyId);
            return await context.TypeContacts.GroupJoin(
                    companies, 
                    tc => tc.Id,
                    c => c.TypeContactId, 
                    (tc, c) => new TypeContactViewData
                    {
                        TypeContactId = tc.Id,
                        TypeContactName = tc.Name.Value,
                        Count = c.Count(),
                        Pourcent = companies.Count() > 0
                            ? (c.Count() / (double)companies.Count()) * 100
                            : 0
                    })
                .ToListAsync();
        }
    }
}