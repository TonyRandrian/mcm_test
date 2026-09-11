using Mcm.Company.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Company.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Domain.Entities.Company>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Company> builder)
    {
        builder.ToTable("companies");

        builder.HasOne(company => company.Parent)
            .WithMany()
            .HasForeignKey(company => company.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(c => c.TypeContact)
            .WithMany()
            .HasForeignKey(v => v.TypeContactId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(c => c.CompanyActivities)
            .WithOne(ca => ca.Company)
            .HasForeignKey(ca => ca.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        
        builder.OwnsOne(e => e.Logo, img =>
        {
            img.Property(i => i.AlternativeText).HasColumnName("logo_alternative_text");
            img.Property(i => i.Url).HasColumnName("logo_url");
            img.Property(i => i.FileType).HasColumnName("logo_file_type");
            img.Property(i => i.StorageType).HasColumnName("logo_storage_type");
        });

        builder.OwnsOne(e => e.Name, nav => 
        {
            nav.Property(x => x.Value)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsMany(c => c.SupplValues, nav =>
        {
            nav.WithOwner()
                .HasForeignKey("CompanyId");
            nav.HasKey(x => x.Id);
            nav.Property(x => x.Id).ValueGeneratedOnAdd();
            nav.Property(x => x.Data)
                .IsRequired();
            nav.Property(x => x.PropertyId)
                .IsRequired();
            nav.Property(x => x.TenantId)
                .IsRequired();
        });
        
        builder.Navigation(c => c.CompanyActivities)
            .HasField("_companyActivities")
            .AutoInclude();
        builder.Navigation(c => c.TypeContact)
            .AutoInclude();
        builder.Navigation(c => c.SupplValues)
            .AutoInclude();
        builder.Navigation(c => c.Logo)
            .AutoInclude();
    }
}