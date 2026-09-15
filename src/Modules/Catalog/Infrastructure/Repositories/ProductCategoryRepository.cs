using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Catalog.Infrastructure.Repositories
{
    public class ProductCategoryRepository(CatalogDbContext context)
        : GenericRepository<ProductCategory>(context), IProductCategoryRepository
    {
        public override Task<ProductCategory?> GetByIdAsync(Guid id)
        {
            IQueryable<ProductCategory> query = _dbSet
                .Include(pc => pc.ProductRelations)
                .Include(pc => pc.ParentCategory);
            return query.FirstOrDefaultAsync(pc => pc.Id == id);
        }
    }
}