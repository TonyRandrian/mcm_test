using Mcm.Property.Domain.Entities;
using Mcm.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Property.Infrastructure.Database
{
    public static class SystemCategorySeeder
    {
        public static async Task SystemDataAsync(PropertyDbContext context)
        {
            var address = await UploadCategoryAsync(context, "Adresse", ["TeamMember", "Company"]);
            var contact = await UploadCategoryAsync(context, "Contact", ["Company"]);

            await UploadPropertyAsync(context, address.Id, "Lot", "Text", true, false);
            await UploadPropertyAsync(context, address.Id, "Ville", "Text", true, false);
            await UploadPropertyAsync(context, address.Id, "Commune", "Text", true, false);
            await UploadPropertyAsync(context, address.Id, "Code Postal", "Number", true, false);
            await UploadPropertyAsync(context, address.Id, "Région", "Text", true, false, previousName: "Region");
            await UploadPropertyAsync(context, contact.Id, "Email", "Email", true, true);
            await UploadPropertyAsync(context, contact.Id, "Téléphone", "PhoneNumber", true, true, previousName: "Telephone");

            await context.SaveChangesAsync();
        }

        private static async Task<Category> UploadCategoryAsync(
            PropertyDbContext context, string name, List<string> entityTypes)
        {
            var existing = await context.Categories
                .Include(c => c.Entities)
                .FirstOrDefaultAsync(c => c.Name.Value == name && c.IsSystem);

            if (existing is null)
            {
                var category = Category.Create(name, isSystem: true);
                category.AddGroupEntityType(entityTypes);
                context.Categories.Add(category);
                return category;
            }

            existing.SyncListEntityType(entityTypes);
            return existing;
        }

        private static async Task UploadPropertyAsync(
            PropertyDbContext context,
            Guid categoryId, 
            string name,
            string type, 
            bool isRequired, 
            bool isMultiple, 
            string? previousName = null)
        {
            var existing = await context.Properties
                .FirstOrDefaultAsync(p => p.CategoryId == categoryId && p.Name.Value.ToLower().Equals(name.ToLower()) && p.IsSystem);
            if (existing is not null)
                return;

            if (previousName is not null)
            {
                var toRename = await context.Properties
                    .FirstOrDefaultAsync(p => p.CategoryId == categoryId && p.Name.Value.ToLower().Equals(previousName.ToLower()) && p.IsSystem);
                if (toRename is not null)
                {
                    toRename.Update(name, description: null, type: null);
                    return;
                }
            }

            context.Properties.Add(Domain.Entities.Property.Create(
                categoryId, name, "", type, isRequired, isMultiple, isSystem: true));
        }
    }
}