using System.Text.Json;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Mcm.Catalog.Infrastructure.Repositories
{
    public class ProductRepository(CatalogDbContext context)
        : GenericRepository<Product>(context), IProductRepository
    {
        public override Task<Product?> GetByIdAsync(Guid id)
        {
            IQueryable<Product> query = _dbSet
                .Include(p => p.Currency)
                .Include(p => p.CategoryRelations)
                .ThenInclude(cr => cr.Category);
            return query.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}