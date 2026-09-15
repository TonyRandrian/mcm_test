using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Catalog.Infrastructure.Repositories
{
    public class ServiceCategoryRepository(CatalogDbContext context)
        : GenericRepository<ServiceCategory>(context), IServiceCategoryRepository
    {
        public override Task<ServiceCategory?> GetByIdAsync(Guid id)
        {
            IQueryable<ServiceCategory> query = _dbSet
                .Include(sc => sc.ParentCategory)
                .Include(sc => sc.Services);
            return query.FirstOrDefaultAsync(sc => sc.Id == id);
        }
    }
}