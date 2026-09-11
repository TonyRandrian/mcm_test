using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Authorizations.Infrastructure.Configurations;

public class RoleCompanyConfiguration : IEntityTypeConfiguration<RoleCompany>
{
    public void Configure(EntityTypeBuilder<RoleCompany> builder)
    {
        builder.ToTable("role_company");

    }
}