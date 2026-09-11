using System.Reflection;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Infrastructure.Configurations;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Interactions.Infrastructure.Database
{
    public class InteractionDbContext(DbContextOptions<InteractionDbContext> options, ITenantProvider tenantProvider, IMediator mediator)
        : DbContext(options)
    {
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly IMediator _mediator = mediator;
        private Guid? TenantId => _tenantProvider.GetTenantId();

        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<InteractionMember> InteractionMembers { get; set; }
        public DbSet<InteractionContact> InteractionContacts { get; set; }
        public DbSet<InteractionType> InteractionTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportMember> ReportMembers { get; set; }
        public DbSet<ReportContact> ReportContacts { get; set; }
        public DbSet<TypeField> TypeFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("interactions");

            modelBuilder.Entity<Interaction>()
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted)
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<InteractionType>()
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted)
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<TypeField>()
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted)
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<Report>()
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted)
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<ReportMember>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<ReportContact>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<InteractionMember>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<InteractionContact>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            
            modelBuilder.ApplyConfiguration(new InteractionConfiguration());
            modelBuilder.ApplyConfiguration(new InteractionMemberConfiguration());
            modelBuilder.ApplyConfiguration(new InteractionContactConfiguration());
            modelBuilder.ApplyConfiguration(new InteractionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new ReportContactConfiguration());
            modelBuilder.ApplyConfiguration(new ReportMemberConfiguration());
            modelBuilder.ApplyConfiguration(new TypeFieldConfiguration());

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