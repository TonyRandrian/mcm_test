using Mcm.Interactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Interactions.Infrastructure.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("interaction_reports");

        builder.OwnsOne(x => x.Name, nav =>
        {
            nav.Property(x => x.Value)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        builder.OwnsOne(e => e.Date, nav =>
        {
            nav.Property(d => d.StartDate)
                .HasColumnName("startDate");
            nav.Property(d => d.EndDate)
                .HasColumnName("endDate");
        });

        builder.OwnsMany(x => x.Attachments, nav =>
        {
            nav.ToTable("report_attachments");
            nav.WithOwner().HasForeignKey("ReportId");

            nav.HasKey("ReportId", "Id");
        });
        
        builder.HasMany(i => i.PresentContacts)
            .WithOne(im => im.Report)
            .HasForeignKey(im => im.ReportId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(i => i.PresentMembers)
            .WithOne(ic => ic.Report)
            .HasForeignKey(ic => ic.ReportId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Interaction)
            .WithOne(i => i.Report)
            .HasForeignKey<Interaction>(i => i.ReportId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Navigation(c => c.PresentContacts)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Navigation(c => c.PresentMembers)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Navigation(c => c.Interaction)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}