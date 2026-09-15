using Mcm.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Catalog.Infrastructure.Configurations;

public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.ToTable("catalog_service_categories");

        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Services)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        // builder.Navigation(e => e.Services)
        //     .AutoInclude()
        //     .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}