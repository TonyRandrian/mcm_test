using Mcm.Contacts.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Contacts.Infrastructure.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contacts");

        builder.OwnsMany(c => c.Values, nav =>
        {
            nav.WithOwner()
                .HasForeignKey("ContactId");
            nav.HasKey(x => x.Id);
            nav.Property(x => x.Id).ValueGeneratedOnAdd();
            nav.Property(x => x.Data)
                .IsRequired();
            nav.Property(x => x.PropertyId)
                .IsRequired();
            nav.Property(x => x.TenantId)
                .IsRequired();
        });

        builder.OwnsOne(contact => contact.Identity, i =>
        {
            i.OwnsOne(x => x.FirstName, nav =>
            {
                nav.Property(x => x.Value)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            });
            i.OwnsOne(x => x.LastName, nav =>
            {
                nav.Property(x => x.Value)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            });
            i.Property(x => x.Position)
                .HasMaxLength(255)
                .HasColumnName("position");
            i.OwnsOne(x => x.Email, nav =>
            {
                nav.Property(x => x.Value)
                .HasMaxLength(255)
                .HasColumnName("email")
                .IsRequired();
            });
        });
        
        builder.OwnsOne(e => e.Image, img =>
        {
            img.Property(i => i.AlternativeText).HasColumnName("image_alternative_text");
            img.Property(i => i.Url).HasColumnName("image_url");
            img.Property(i => i.FileType).HasColumnName("image_file_type");
            img.Property(i => i.StorageType).HasColumnName("image_storage_type");
        });

        // builder.Navigation(c => c.Values)
        //     .HasField("_values")
        //     .AutoInclude()
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);
        // builder.Navigation(c => c.Identity)
        //     .AutoInclude()
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}