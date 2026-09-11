using Mcm.Property.Infrastructure.Database;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Property.Infrastructure.Repositories
{
    public class PropertyModule(PropertyDbContext context)
        : IPropertyModule
    {
        private readonly PropertyDbContext _context = context;
        public async Task<PropertyDto?> GetByIdAsync(Guid propertyId)
        {
            return await _context.Properties.AsNoTracking()
                .Where(p => p.Id == propertyId)
                .Select(p => new PropertyDto(
                    p.Id,
                    p.Name,
                    p.IsSystem,
                    p.IsRequired,
                    p.IsMultiple,
                    p.Type))
                .FirstOrDefaultAsync();
        }

        public Task<PropertyDto?> PropertyInCategory(Guid propertyId, Guid categoryId)
        {
            return _context.Properties.AsNoTracking(    )
                .Where(p => p.CategoryId == categoryId && p.Id == propertyId)
                .Select(p => new PropertyDto(
                    p.Id,
                    p.Name,
                    p.IsSystem,
                    p.IsRequired,
                    p.IsMultiple,
                    p.Type))
                .FirstOrDefaultAsync();
        }
    }
}