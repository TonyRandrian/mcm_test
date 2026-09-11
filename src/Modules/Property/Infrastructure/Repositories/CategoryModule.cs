using Mcm.Property.Infrastructure.Database;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Property.Infrastructure.Repositories
{
    public class CategoryModule(PropertyDbContext context)
        : ICategoryModule
    {
        private readonly PropertyDbContext _context = context;
        public async Task<List<CategoryDto>> GetAllAsync(List<Guid> propertyIds)
        {
            var properties = await _context.Properties
                        .AsNoTracking()
                        .Where(p => propertyIds.Contains(p.Id))
                        .ToListAsync();
            var grpProperties = properties.GroupBy(p => p.Category);
            return grpProperties
                .Select(c => new CategoryDto
                (
                    c.Key.Id,
                    c.Key.Name,
                    Properties: c.Select(p => new PropertyDto
                    (
                        Id: p.Id,
                        Name: p.Name,
                        IsSystem: p.IsSystem,
                        IsRequired: p.IsRequired,
                        IsMultiple: p.IsMultiple,
                        Type: p.Type
                    ))
                ))
                .ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid categoryId)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();

            if (category is null)
                return null;

            return new CategoryDto(
                category.Id,
                category.Name,
                Properties: category.Properties.Select(p => new PropertyDto(
                    Id: p.Id,
                    Name: p.Name,
                    IsSystem: p.IsSystem,
                    IsRequired: p.IsRequired,
                    IsMultiple: p.IsMultiple,
                    Type: p.Type
                ))
            );
        }
    }
}