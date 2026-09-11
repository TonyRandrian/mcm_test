using Mcm.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Catalog.Infrastructure.Configurations
{
    public class ProductCategoryRelationConfiguration : IEntityTypeConfiguration<ProductCategoryRelation>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryRelation> builder)
        {
            builder.ToTable("relation_product_categories");

            builder.HasKey(e => new { e.ProductId, e.CategoryId });

            builder.HasOne(e => e.Product)
                .WithMany(p => p.CategoryRelations)
                .HasForeignKey(e => e.ProductId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(e => e.Category)
                .WithMany(p => p.ProductRelations)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Navigation(e => e.Product)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            builder.Navigation(e => e.Category)
                .AutoInclude()
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}