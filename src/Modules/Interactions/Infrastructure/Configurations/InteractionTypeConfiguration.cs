using Mcm.Interactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Interactions.Infrastructure.Configurations;

public class InteractionTypeConfiguration : IEntityTypeConfiguration<InteractionType>
{
    public void Configure(EntityTypeBuilder<InteractionType> builder)
    {
        builder.ToTable("interaction_interactionTypes");

        builder.OwnsOne(x => x.Title, nav =>
        {
            nav.Property(x => x.Value)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        builder.HasMany(it => it.Fields)
            .WithOne(tf => tf.InteractionType)
            .HasForeignKey(tf => tf.InteractionTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Fields)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}