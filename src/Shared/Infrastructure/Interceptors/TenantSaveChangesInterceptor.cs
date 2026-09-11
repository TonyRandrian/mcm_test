using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Mcm.Shared.Infrastructure.Interceptors;

public class TenantSaveChangesInterceptor(
    ITenantProvider tenantProvider) : SaveChangesInterceptor
{
    private readonly ITenantProvider _tenantProvider = tenantProvider;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);

        var tenantId = _tenantProvider.GetTenantId();

        if (tenantId is null)
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);

        foreach (var entry in context.ChangeTracker
                     .Entries<ITenantScoped>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = tenantId.Value;
            }
        }

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }
}