using Mcm.Interactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Interactions.Infrastructure.Configurations;

public class InteractionContactConfiguration : IEntityTypeConfiguration<InteractionContact>
{
    public void Configure(EntityTypeBuilder<InteractionContact> builder)
    {
        builder.ToTable("interaction_contacts");

        builder.HasKey(ic => new{ ic.InteractionId, ic.ContactId });
    }
}