using Mcm.Property.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Property.Infrastructure.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category_value");

        builder.OwnsOne(e => e.Name, nav =>
        {
            nav.Property(x => x.Value)
                .HasColumnName("name")
                .HasMaxLength(100);
        });
            

        builder.HasMany(category => category.Properties)
                .WithOne(property => property.Category)
                .HasForeignKey(category => category.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(category => category.Entities)
                .WithOne(catEnt => catEnt.Category)
                .HasForeignKey(catEnt => catEnt.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        
        // builder.Navigation(c => c.Properties)
        //     .AutoInclude();
        // builder.Navigation(c => c.Entities)
        //     .AutoInclude();
    }
}