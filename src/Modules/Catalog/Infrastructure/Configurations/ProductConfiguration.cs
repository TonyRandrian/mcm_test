using Mcm.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Catalog.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("catalog_products");

        builder.HasKey(e => e.Id);

        builder.OwnsOne(e => e.CoverPicture, img =>
        {
            img.Property(i => i.AlternativeText).HasColumnName("image_alternative_text");
            img.Property(i => i.Url).HasColumnName("image_url");
            img.Property(i => i.FileType).HasColumnName("image_file_type");
            img.Property(i => i.StorageType).HasColumnName("image_storage_type");
        });

        builder.OwnsMany(x => x.Images, img =>
        {
            img.ToTable("catalog_product_images");
            img.WithOwner().HasForeignKey("ProductId"); 
            
            img.Property(i => i.Url);
            img.Property(i => i.FileType);

            img.HasKey("ProductId", "Id");
        });

        builder.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId);
            // .OnDelete(DeleteBehavior.NoAction);
        
        // builder.Navigation(e => e.Currency)
        //     .AutoInclude();
        // builder.Navigation(e => e.CategoryRelations)
        //     .AutoInclude()
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}