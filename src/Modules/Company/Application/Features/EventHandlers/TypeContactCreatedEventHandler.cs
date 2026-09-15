using Mcm.Company.Application.Interfaces;
using Mcm.Company.Domain.Events;
using Mcm.Shared.Domain.Events;
using MediatR;

namespace Mcm.Company.Application.Features.EventHandlers
{
    public class TypeContactCreatedEventHandler(
        ICompanyDashboardService companyDashboardService)
        : INotificationHandler<TypeContactCreatedEvent>
    {
        private readonly ICompanyDashboardService _companyDashboardService = companyDashboardService;
        public async Task Handle(TypeContactCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _companyDashboardService.SendViewData();
        }
    }
}