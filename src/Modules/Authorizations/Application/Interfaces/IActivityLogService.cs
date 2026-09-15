using Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs;
using Mcm.Authorizations.Domain.Entities;

namespace Mcm.Authorizations.Application.Interfaces
{
    public interface IActivityLogService
    {
        Task SendActivityLog(ActivityLogResponse log);
    }
}