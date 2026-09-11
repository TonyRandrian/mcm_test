using Mcm.Property.Application.Interfaces;
using Mcm.Property.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Property.Infrastructure.Repositories
{
    public class PropertyUow(PropertyDbContext context)
        : UnitOfWork<PropertyDbContext>(context), IPropertyUow
    {
    }
}