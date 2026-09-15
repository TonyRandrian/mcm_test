using Mcm.Property.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Property.Infrastructure.Configurations
{
    public class CategorySettingConfiguration : IEntityTypeConfiguration<CategorySetting>
{
    public void Configure(EntityTypeBuilder<CategorySetting> builder)
    {
        builder.ToTable("category_profile_setting");

        builder.HasIndex(x => new { x.TenantId, x.CategoryId, x.EntityType })
            .IsUnique();

        builder.HasOne(cs => cs.Category)
            .WithMany()
            .HasForeignKey(cs => cs.CategoryId);
    }
}
}