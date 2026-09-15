using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Catalog.Infrastructure.Repositories
{
    public class CatalogUow(CatalogDbContext context)
        : UnitOfWork<CatalogDbContext>(context), ICatalogUow
    {
    }
}