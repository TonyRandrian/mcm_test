using Mcm.Property.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Property.Infrastructure.Configurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("category_entity");

        // builder.HasOne(catEnt => catEnt.Category)
        //         .WithMany(category => category.Entities)
        //         .HasForeignKey(catEnt => catEnt.CategoryId)
        //         .OnDelete(DeleteBehavior.Restrict);
    }

}