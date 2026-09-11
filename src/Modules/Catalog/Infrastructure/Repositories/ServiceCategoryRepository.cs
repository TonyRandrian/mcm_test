using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Catalog.Infrastructure.Repositories
{
    public class ServiceCategoryRepository(CatalogDbContext context)
        : GenericRepository<ServiceCategory>(context), IServiceCategoryRepository
    {
    }
}