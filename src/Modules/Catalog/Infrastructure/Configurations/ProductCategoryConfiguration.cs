using Mcm.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Catalog.Infrastructure.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("catalog_product_categories");

        builder.HasKey(e => e.Id);

        builder.Navigation(e => e.ProductRelations)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}