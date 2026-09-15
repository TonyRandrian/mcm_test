using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Authorizations.Infrastructure.Repositories
{
    public class ActivityLogRepository(AuthorizationDbContext context)
        : GenericRepository<ActivityLog>(context), IActivityLogRepository
    {
    }
}