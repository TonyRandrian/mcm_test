using Mcm.Interactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Interactions.Infrastructure.Configurations;

public class ReportMemberConfiguration : IEntityTypeConfiguration<ReportMember>
{
    public void Configure(EntityTypeBuilder<ReportMember> builder)
    {
        builder.ToTable("report_members");

        builder.HasKey(rt => new{ rt.ReportId, rt.TeamMemberId });
    }
}