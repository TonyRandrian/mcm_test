using System.Reflection;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Authorizations.Infrastructure.Configurations;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Mcm.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Authorizations.Infrastructure.Database
{
    public class AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options, ITenantProvider tenantProvider, IMediator mediator)
        : DbContext(options)
    {
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly IMediator _mediator = mediator;
        private Guid? TenantId => _tenantProvider.GetTenantId();

        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoleCompany> RoleCompanies { get; set; }
        public DbSet<RoleTeamMember> RoleTeamMembers { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("authorizations");

            modelBuilder.Entity<Role>()
                .HasQueryFilter("MultiTenant",  e => e.TenantId == TenantId)
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted);
            modelBuilder.Entity<RoleCompany>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<RoleTeamMember>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<Permission>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<PasswordReset>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId);
            modelBuilder.Entity<TeamMember>()
                .HasQueryFilter("MultiTenant", e => e.TenantId == TenantId)
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted);

            modelBuilder.ApplyConfiguration(new TeamMemberConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new RoleTeamMemberConfiguration());

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