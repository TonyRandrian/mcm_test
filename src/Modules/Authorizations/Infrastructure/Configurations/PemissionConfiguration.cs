using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Authorizations.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.Property(p => p.Action).HasConversion<string>();
        builder.Property(p => p.Module).HasConversion<string>();

    }
}