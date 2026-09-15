using System.Text.Json;
using Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.Interfaces;
using MediatR;

namespace Mcm.Authorizations.Application.Features.EventHandler
{
    public class ActivityLogHandler(
        ICurrentUserService currentUserService,
        ITeamMemberModule teamMemberModule,
        IActivityLogRepository activityLogRepository,
        IActivityLogService activityLogService,
        IAuthorizationUow uow)
        : INotificationHandler<IDomainEvent>
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IActivityLogRepository _activityLogRepository = activityLogRepository;
        private readonly IActivityLogService _activityLogService = activityLogService;
        private readonly ITeamMemberModule _teamMemberModule = teamMemberModule;
        private readonly IAuthorizationUow _uow = uow;
        
        public async Task Handle(IDomainEvent notification, CancellationToken cancellationToken)
        {
            var activityLog = ActivityLog.Create(
                eventType: notification.GetType().Name,
                tenantId: _currentUserService.TenantId,
                companyId: _currentUserService.CompanyId,
                authorId: _currentUserService.TeamMemberId,
                payloadJson: JsonSerializer.Serialize(notification, notification.GetType())
            );
            await _activityLogRepository.AddAsync(activityLog);
            await _uow.SaveChangesAsync(cancellationToken);

            var (category, label) = EventDescriptor.Describe(activityLog.EventType);
            var tm = (await _teamMemberModule.GetIdentities([activityLog.AuthorId])).FirstOrDefault();
            var result = new ActivityLogResponse
            {
                Id = activityLog.Id,
                EventType = activityLog.EventType,
                Category = category,
                Label = label,
                OccuredAt = activityLog.OccuredAt,
                Detail = EventDescriptor.Detail(activityLog.EventType, activityLog.PayloadJson),
                Author = new ActivityLogs_Author
                {
                    Id = activityLog.AuthorId,
                    FullName = $"{tm?.FirstName} {tm?.LastName}"
                }
            };

            await _activityLogService.SendActivityLog(result);
        }
    }
}