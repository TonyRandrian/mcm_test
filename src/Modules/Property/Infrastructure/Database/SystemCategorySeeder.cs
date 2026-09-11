using Mcm.Property.Domain.Entities;
using Mcm.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Property.Infrastructure.Database
{
    public static class SystemCategorySeeder
    {
        public static async Task SystemDataAsync(PropertyDbContext context)
        {
            if (await context.Categories.AnyAsync())
                return;
            
            var address = Category.Create("Adresse", isSystem: true);
            address.AddGroupEntityType(["TeamMember", "Company"]);
            var contact = Category.Create("Contact", isSystem: true);
            contact.AddGroupEntityType(["Company"]);
            context.Categories.AddRange(address, contact);
        
            var properties = new List<Domain.Entities.Property>
            {
                Domain.Entities.Property.Create(
                    categoryId: address.Id,
                    name: "Lot",
                    description: "",
                    type: "Text", 
                    isRequired: true,
                    isMultiple: false,
                    isSystem: true),
                Domain.Entities.Property.Create(address.Id, "Ville", "", "Text", true, false, true),
                Domain.Entities.Property.Create(address.Id, "Commune", "", "Text", true, false, true),
                Domain.Entities.Property.Create(address.Id, "Code Postal", "", "Number", true, false, true),
                Domain.Entities.Property.Create(address.Id, "Region", "", "Text", true, false, true),
                Domain.Entities.Property.Create(contact.Id, "Email", "", "Text", true, true, true),
                Domain.Entities.Property.Create(contact.Id, "Telephone", "", "Text", true, true, true),
            };
            context.Properties.AddRange(properties);

            await context.SaveChangesAsync();
        }
    }
}