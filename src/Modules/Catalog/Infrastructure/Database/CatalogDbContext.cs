using System.Reflection;
using Mcm.Catalog.Domain.Entities;
using Mcm.Catalog.Infrastructure.Configurations;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Mcm.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Catalog.Infrastructure.Database
{
    public class CatalogDbContext(
        DbContextOptions<CatalogDbContext> options, 
        ITenantProvider tenantProvider,
        IMediator mediator)
        : DbContext(options)
    {
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private Guid? TenantId => _tenantProvider.GetTenantId();
        private readonly IMediator _mediator = mediator;

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<ProductCategoryRelation> ProductCategoryRelations => Set<ProductCategoryRelation>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<Currency> Currencies => Set<Currency>();

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ITenantScoped>())
            {
                if (entry.State == EntityState.Added && TenantId != Guid.Empty && TenantId.HasValue)
                    entry.Entity.TenantId = TenantId.Value;
    
            }
            
            var result = await base.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventAsync(this);
            return result;
        }
    }
}