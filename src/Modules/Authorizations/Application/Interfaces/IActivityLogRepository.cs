using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Interfaces;

namespace Mcm.Authorizations.Application.Interfaces
{
    public interface IActivityLogRepository
        : IGenericRepository<ActivityLog>
    {
    }
}