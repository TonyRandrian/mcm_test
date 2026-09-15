using System.Text.Json;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Catalog.Infrastructure.Repositories
{
    public class ServiceRepository(CatalogDbContext context)
        : GenericRepository<Service>(context), IServiceRepository
    {
        public override Task<Service?> GetByIdAsync(Guid id)
        {
            IQueryable<Service> query = _dbSet
                .Include(s => s.Currency)
                .Include(s => s.Category);
            return query.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}