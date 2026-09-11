using Mcm.Interactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Interactions.Infrastructure.Configurations;

public class TypeFieldConfiguration : IEntityTypeConfiguration<TypeField>
{
    public void Configure(EntityTypeBuilder<TypeField> builder)
    {
        builder.ToTable("interaction_typeFields");

        builder.OwnsOne(x => x.Name, nav =>
        {
            nav.Property(x => x.Value)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

    }
}