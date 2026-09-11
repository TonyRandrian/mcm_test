using Mcm.Property.Application.Interfaces;
using Mcm.Property.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Property.Infrastructure.Repositories
{
    public class PropertyRepository(PropertyDbContext context)
        : GenericRepository<Domain.Entities.Property>(context), IPropertyRepository
    {
    }
}