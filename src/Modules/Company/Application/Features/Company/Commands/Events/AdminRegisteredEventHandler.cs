using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Events;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.Events
{
    public class AdminRegisteredEventHandler(
        ICompanyRepository companyRepository,
        ICompanyUow uow)
        : INotificationHandler<AdminRegisteredEvent>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ICompanyUow _uow = uow;

        public async Task Handle(AdminRegisteredEvent notif, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.FindCompanyByIdOutTenant(notif.CompanyId)
                ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), notif.CompanyId);
            company.UpdateLeader(notif.TeamMemberId);

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}