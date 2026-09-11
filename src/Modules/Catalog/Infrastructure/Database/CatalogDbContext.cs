using System.Reflection;
using Mcm.Catalog.Domain.Entities;
using Mcm.Catalog.Infrastructure.Configurations;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Mcm.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Catalog.Infrastructure.Database
{
    public class CatalogDbContext(DbContextOptions<CatalogDbContext> options, ITenantProvider tenantProvider)
        : DbContext(options)
    {
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private Guid? TenantId => _tenantProvider.GetTenantId();

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryRelation> ProductCategoryRelations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Catalog");

            modelBuilder.Entity<Product>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId)
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted);
            modelBuilder.Entity<Service>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId)
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted);
            modelBuilder.Entity<ProductCategory>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId)
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted);
            modelBuilder.Entity<ServiceCategory>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId)
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted);
            modelBuilder.Entity<ProductCategoryRelation>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);

            
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryRelationConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceCategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                Console.WriteLine($"{entry.Entity.GetType().Name} - {entry.State}");
    
            }
            foreach (var entry in ChangeTracker.Entries<ITenantScoped>())
            {
                if (entry.State == EntityState.Added && TenantId != Guid.Empty && TenantId.HasValue)
                    entry.Entity.TenantId = TenantId.Value;
    
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}