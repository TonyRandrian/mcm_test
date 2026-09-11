using Mcm.Interactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Interactions.Infrastructure.Configurations;

public class InteractionMemberConfiguration : IEntityTypeConfiguration<InteractionMember>
{
    public void Configure(EntityTypeBuilder<InteractionMember> builder)
    {
        builder.ToTable("interaction_Members");

        builder.HasKey(im => new{ im.InteractionId, im.TeamMemberId });
    }
}