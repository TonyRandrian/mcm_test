using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Authorizations.Infrastructure.Configurations;

public class RoleTeamMemberConfiguration : IEntityTypeConfiguration<RoleTeamMember>
{
    public void Configure(EntityTypeBuilder<RoleTeamMember> builder)
    {
        builder.ToTable("role_teammember");

        builder.HasKey(rt => new{ rt.RoleId, rt.TeamMemberId });

        builder.Navigation(tm => tm.Role)
            .AutoInclude();
    }
}