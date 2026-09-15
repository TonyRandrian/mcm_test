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

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RoleCompany> RoleCompanies => Set<RoleCompany>();
        public DbSet<RoleTeamMember> RoleTeamMembers => Set<RoleTeamMember>();
        public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<PasswordReset> PasswordResets => Set<PasswordReset>();
        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("authorizations");

            modelBuilder.Entity<Role>()
                .HasQueryFilter("MultiTenant",  e => e.TenantId == TenantId)
                .HasQueryFilter("SoftDelete", e => !e.IsDeleted);
            modelBuilder.Entity<ActivityLog>()
                .HasQueryFilter("MultiTenant",  e => e.TenantId == TenantId);
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
            modelBuilder.Entity<RefreshToken>();

            modelBuilder.ApplyConfiguration(new TeamMemberConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new RoleTeamMemberConfiguration());

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