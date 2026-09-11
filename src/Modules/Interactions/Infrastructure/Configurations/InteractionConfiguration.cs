using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Interactions.Infrastructure.Configurations;

public class InteractionConfiguration : IEntityTypeConfiguration<Interaction>
{
    public void Configure(EntityTypeBuilder<Interaction> builder)
    {
        builder.ToTable("interactions");

        builder.OwnsOne(x => x.Title, nav =>
        {
            nav.Property(x => x.Value)
                .HasMaxLength(255)
                .HasColumnName("title");
        });
        
        builder.OwnsOne(e => e.Date, nav =>
        {
            nav.Property(d => d.StartDate)
                .HasColumnName("startDate");
            nav.Property(d => d.EndDate)
                .HasColumnName("endDate");
        });
        builder.OwnsOne(e => e.Reminder, nav =>
        {
            nav.Property(d => d.Type)
                .HasColumnName("reminder_type");
            nav.Property(d => d.Value)
                .HasColumnName("reminder_value");
            nav.Property(d => d.Repeat)
                .HasColumnName("reminder_repeat");
        });

        builder.OwnsMany(x => x.Attachments, nav =>
        {
            nav.ToTable("interaction_attachments");
            nav.WithOwner().HasForeignKey("InteractionId");

            nav.HasKey("InteractionId", "Id");
        });

        builder.OwnsMany(x => x.FieldsValues, nav =>
        {
            nav.ToTable("interaction_fieldsValues");
            nav.WithOwner().HasForeignKey("InteractionId");

            nav.HasKey("InteractionId", "Id");
        });
        
        builder.HasOne(i => i.Report)
            .WithOne(r => r.Interaction)
            .HasForeignKey<Report>(r => r.InteractionId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(i => i.InteractionMembers)
            .WithOne(im => im.Interaction)
            .HasForeignKey(im => im.InteractionId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(i => i.InteractionContacts)
            .WithOne(ic => ic.Interaction)
            .HasForeignKey(ic => ic.InteractionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Type)
            .WithMany()
            .HasForeignKey(i => i.TypeId);

        builder.Navigation(c => c.Type)
            .AutoInclude();
        builder.Navigation(c => c.InteractionMembers)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Navigation(c => c.InteractionContacts)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Navigation(i => i.Report)
            .AutoInclude();
    }
}