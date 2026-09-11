using System.Reflection;
using Mcm.Contacts.Domain.Entities;
using Mcm.Contacts.Infrastructure.Configurations;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Contacts.Infrastructure.Database
{
    public class ContactDbContext(DbContextOptions<ContactDbContext> options, ITenantProvider tenantProvider, IMediator mediator)
        : DbContext(options)
    {
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly IMediator _mediator = mediator;
        private Guid? TenantId => _tenantProvider.GetTenantId();
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("contacts");

            modelBuilder.Entity<Contact>()
                        .HasQueryFilter("SoftDelete", e => !e.IsDeleted)
                        .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);

            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            // modelBuilder.ApplyConfiguration(new ContactValueConfiguration());

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