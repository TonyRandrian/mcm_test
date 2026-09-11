using System.Reflection;
using Mcm.Company.Domain.Entities;
using Mcm.Company.Infrastructure.Configurations;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Mcm.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Company.Infrastructure.Database
{
    public class CompanyDbContext(
        DbContextOptions<CompanyDbContext> options, 
        ITenantProvider tenantProvider,
        IMediator mediator)
        : DbContext(options)
    {
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly IMediator _mediator = mediator;
        private Guid? TenantId => _tenantProvider.GetTenantId();

        public DbSet<Domain.Entities.Company> Companies { get; set; }
        // public DbSet<CompanyValue> CompanyValues { get; set; }
        public DbSet<TypeContact> TypeContacts { get; set; }
        public DbSet<ActivitySector> ActivitySectors { get; set; }
        public DbSet<CompanyActivity> CompanyActivities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Company");

            modelBuilder.Entity<TypeContact>()
                        .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId)
                        .HasQueryFilter("SoftDelete", e => !e.IsDeleted);

            modelBuilder.Entity<Domain.Entities.Company>()
                        .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId)
                        .HasQueryFilter("SoftDelete", e => !e.IsDeleted);
            // modelBuilder.Entity<CompanyValue>()
            //             .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<CompanyActivity>()
                        .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            // modelBuilder.ApplyConfiguration(new CompanyValueConfiguration());
            modelBuilder.ApplyConfiguration(new TypeContactConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyActivityConfiguration());


            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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

            await _mediator.DispatchDomainEventAsync(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}