using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Authorizations.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.OwnsOne(r => r.Title, nav =>
        {
            nav.Property(x => x.Value)
                .HasColumnName("title")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.HasMany(role => role.Members)
            .WithOne(rm => rm.Role)
            .HasForeignKey(rm => rm.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(role => role.Authorizations)
            .WithOne(p => p.Role)
            .HasForeignKey(authorization => authorization.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(role => role.RoleCompanies)
            .WithOne()
            .HasForeignKey(rc => rc.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // builder.Navigation(role => role.Members)
        //     .HasField("_members")
        //     .AutoInclude()
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);
        // builder.Navigation(role => role.Authorizations)
        //     .HasField("_authorizations")
        //     .AutoInclude()
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);
        // builder.Navigation(role => role.RoleCompanies)
        //     .HasField("_roleCompanies")
        //     .AutoInclude()
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);

    }
}