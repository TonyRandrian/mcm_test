using Mcm.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Catalog.Infrastructure.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("catalog_services");

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
            img.ToTable("catalog_service_images"); 
            img.WithOwner().HasForeignKey("ServiceId");

            img.Property(i => i.Url);
            img.Property(i => i.FileType);

            img.HasKey("ServiceId", "Id");
        });

        builder.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId);

        builder.HasOne(e => e.Category)
            .WithMany(cat => cat.Services)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Navigation(e => e.Currency)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Navigation(e => e.Category)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}