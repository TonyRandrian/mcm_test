using Mcm.Shared.Application.Modules.DTOs;

namespace Mcm.Shared.Application.Modules
{
    public interface ICategoryModule
    {
        Task<List<CategoryDto>> GetAllAsync(List<Guid> propertyIds);
        Task<CategoryDto?> GetByIdAsync(Guid categoryId);
    }
}