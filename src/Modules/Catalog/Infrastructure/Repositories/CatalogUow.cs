using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Catalog.Infrastructure.Repositories
{
    public class CatalogUow(CatalogDbContext context)
        : UnitOfWork<CatalogDbContext>(context), ICatalogUow
    {
    }
}