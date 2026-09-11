using Mcm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Company.Infrastructure.Configurations
{
    public class CompanyActivityConfiguration : IEntityTypeConfiguration<CompanyActivity>
    {
        public void Configure(EntityTypeBuilder<CompanyActivity> builder)
        {
            builder.ToTable("company_activities");

            builder.HasKey(ca => new { ca.CompanyId, ca.ActivitySectorId });

            builder.HasOne(ca => ca.ActivitySector)
                .WithMany()
                .HasForeignKey(ca => ca.ActivitySectorId)
                .OnDelete(DeleteBehavior.SetNull);
            
            builder.Navigation(ca => ca.ActivitySector)
                .AutoInclude();
        }
    }
}