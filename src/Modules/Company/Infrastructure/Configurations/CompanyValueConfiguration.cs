using Mcm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Company.Infrastructure.Configurations;

public class CompanyValueConfiguration : IEntityTypeConfiguration<CompanyValue>
{
    public void Configure(EntityTypeBuilder<CompanyValue> builder)
    {
        builder.ToTable("company_values");

    }
}