using Mcm.Company.Domain.Entities;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Company.Infrastructure.Configurations;

public class TypeContactConfiguration : IEntityTypeConfiguration<TypeContact>
{
    public void Configure(EntityTypeBuilder<TypeContact> builder)
    {
        builder.ToTable("company_typecontacts");

        builder.OwnsOne(e => e.Name, nav =>
        {
            nav.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.HasOne(tc => tc.TypeConvertToNavigation)
            .WithMany()
            .HasForeignKey(tc => tc.TypeConvertTo)
            .OnDelete(DeleteBehavior.NoAction);

        // builder.Navigation(tc => tc.TypeConvertToNavigation)
        //     .AutoInclude();
    }
}