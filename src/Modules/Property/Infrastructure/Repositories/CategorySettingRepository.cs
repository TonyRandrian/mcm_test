using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Entities;
using Mcm.Property.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Property.Infrastructure.Repositories
{
    public class CategorySettingRepository(PropertyDbContext context)
        : GenericRepository<CategorySetting>(context), ICategorySettingRepository
    {
    }
}