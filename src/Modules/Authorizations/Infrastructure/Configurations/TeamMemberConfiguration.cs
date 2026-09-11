using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Authorizations.Infrastructure.Configurations;

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.ToTable("teammembers");

        builder.OwnsOne(e => e.Identity, i =>
        {
            i.Property(x => x.FirstName)
                .HasConversion<string>(name => name.Value, value => new Name(value))
                .HasMaxLength(255)
                .HasColumnName("first_name");
            i.Property(x => x.LastName)
                .HasConversion<string>(name => name.Value, value => new Name(value))
                .HasMaxLength(255)
                .HasColumnName("last_name");
            i.Property(x => x.Position).HasMaxLength(255).HasColumnName("position");
            i.Property(x => x.Email)
            .HasColumnName("email")
            .HasConversion(
                email => email.Value,
                value => new Email(value))
            .HasMaxLength(255)
            .IsRequired();
        });

        builder.OwnsOne(e => e.Image, img =>
        {
            img.Property(i => i.AlternativeText).HasColumnName("image_alt");
            img.Property(i => i.FileType).HasConversion<string>().HasColumnName("image_file");
            img.Property(i => i.Url).HasColumnName("image_url");
            img.Property(i => i.StorageType).HasConversion<string>().HasColumnName("image_storage");
        });

        builder.OwnsMany(c => c.SupplValues, nav =>
        {
            nav.WithOwner()
                .HasForeignKey("TeamMemberId");
            nav.HasKey(x => x.Id);
            nav.Property(x => x.Id).ValueGeneratedOnAdd();
            nav.Property(x => x.Data)
                .IsRequired();
            nav.Property(x => x.PropertyId)
                .IsRequired();
            nav.Property(x => x.TenantId)
                .IsRequired();
        });

        builder.HasMany(tm => tm.Roles)
            .WithOne(rt => rt.TeamMember)
            .HasForeignKey(rt => rt.TeamMemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(tm => tm.SupplValues)
            .HasField("_values")
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(tm => tm.Roles)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);

    }
}