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

        public DbSet<Interaction> Interactions => Set<Interaction>();
        public DbSet<InteractionMember> InteractionMembers => Set<InteractionMember>();
        public DbSet<InteractionContact> InteractionContacts => Set<InteractionContact>();
        public DbSet<InteractionType> InteractionTypes => Set<InteractionType>();
        public DbSet<Report> Reports => Set<Report>();
        public DbSet<ReportMember> ReportMembers => Set<ReportMember>();
        public DbSet<ReportContact> ReportContacts => Set<ReportContact>();
        public DbSet<TypeField> TypeFields => Set<TypeField>();

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