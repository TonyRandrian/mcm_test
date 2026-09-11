using Mcm.Interactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Interactions.Infrastructure.Configurations;

public class ReportContactConfiguration : IEntityTypeConfiguration<ReportContact>
{
    public void Configure(EntityTypeBuilder<ReportContact> builder)
    {
        builder.ToTable("report_contacts");

        builder.HasKey(rc => new{ rc.ReportId, rc.ContactId });
    }
}