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
                    Id: p.Id,
                    Name: p.Name,
                    IsSystem: p.IsSystem,
                    IsRequired: p.IsRequired,
                    IsMultiple: p.IsMultiple,
                    IsSensitive: p.IsSensitive,
                    Type: p.Type))
                .FirstOrDefaultAsync();
        }

        public async Task<Dictionary<Guid, PropertyDto>> GetByIdsAsync(List<Guid> propertyIds)
        {
            var properties = await _context.Properties.AsNoTracking()
                .Where(p => propertyIds.Contains(p.Id))
                .Select(p => new PropertyDto(
                    Id: p.Id,
                    Name: p.Name,
                    IsSystem: p.IsSystem,
                    IsRequired: p.IsRequired,
                    IsMultiple: p.IsMultiple,
                    IsSensitive: p.IsSensitive,
                    Type: p.Type))
                .ToListAsync();
            return properties.ToDictionary(p => p.Id);
        }

        public Task<PropertyDto?> PropertyInCategory(Guid propertyId, Guid categoryId)
        {
            return _context.Properties.AsNoTracking(    )
                .Where(p => p.CategoryId == categoryId && p.Id == propertyId)
                .Select(p => new PropertyDto(
                    Id: p.Id,
                    Name: p.Name,
                    IsSystem: p.IsSystem,
                    IsRequired: p.IsRequired,
                    IsMultiple: p.IsMultiple,
                    IsSensitive: p.IsSensitive,
                    Type: p.Type))
                .FirstOrDefaultAsync();
        }
    }
}