using System.Reflection;
using Mcm.Property.Domain.Entities;
using Mcm.Property.Infrastructure.Configurations;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Property.Infrastructure.Database
{
    public class PropertyDbContext(DbContextOptions<PropertyDbContext> options, ITenantProvider tenantProvider)
        : DbContext(options)
    {
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private Guid? TenantId => _tenantProvider.GetTenantId();
        
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Domain.Entities.Property> Properties => Set<Domain.Entities.Property>();
        public DbSet<CategoryEntity> CategoryEntities => Set<CategoryEntity>();
        public DbSet<CategorySetting> CategorySettings => Set<CategorySetting>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("properties");
            modelBuilder.Entity<Category>()
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted)
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId || e.IsSystem);
            modelBuilder.Entity<Domain.Entities.Property>()
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted)
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId || e.IsSystem);
            modelBuilder.Entity<CategoryEntity>()
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted)
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId || e.TenantId == Guid.Empty);
            modelBuilder.Entity<CategorySetting>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
                    
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategorySettingConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ITenantScoped>())
            {
                if (entry.State == EntityState.Added && TenantId != Guid.Empty && TenantId.HasValue)
                    entry.Entity.TenantId = TenantId.Value;
    
            }
        
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}