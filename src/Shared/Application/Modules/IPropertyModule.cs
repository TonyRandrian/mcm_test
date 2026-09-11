using Mcm.Shared.Application.Modules.DTOs;

namespace Mcm.Shared.Application.Modules
{
    public interface IPropertyModule
    {
        Task<PropertyDto?> GetByIdAsync(Guid propertyId);
        Task<PropertyDto?> PropertyInCategory(Guid propertyId, Guid categoryId);
    }
}