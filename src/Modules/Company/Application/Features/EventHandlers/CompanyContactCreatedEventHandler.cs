using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Domain.Events;
using MediatR;

namespace Mcm.Company.Application.Features.EventHandlers
{
    public class RefreshDashBoardEventHandler(
        ICompanyDashboardService companyDashboardService)
        : INotificationHandler<CompanyContactCreatedEvent>
    {
        private readonly ICompanyDashboardService _companyDashboardService = companyDashboardService;
        public async Task Handle(CompanyContactCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _companyDashboardService.SendViewData();
        }
    }
}