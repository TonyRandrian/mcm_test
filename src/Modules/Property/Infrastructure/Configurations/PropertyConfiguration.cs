using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Property.Infrastructure.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Domain.Entities.Property>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Property> builder)
    {
        builder.ToTable("property_definition");

        builder.OwnsOne(e => e.Name, nav =>
        {
            nav.Property(x => x.Value)
                .HasColumnName("name")
                .HasMaxLength(100);
        });
            
        // builder.HasOne(property => property.Category)
        //         .WithMany(category => category.Properties)
        //         .HasForeignKey(property => property.CategoryId)
        //         .OnDelete(DeleteBehavior.Restrict);
        builder.Navigation(p => p.Category)
            .AutoInclude();
    }

}