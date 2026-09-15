using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Modules.DTOs;

namespace Mcm.Shared.Application.Modules
{
    public interface ICategoryModule
    {
        Task<List<CategoryDto>> GetAllAsync(List<Guid> propertyIds);
        Task<HashSet<Guid>> GetVisibleCategoryIdsAsync(EntityType entityType);
        Task<CategoryDto?> GetByIdAsync(Guid categoryId);
    }
}