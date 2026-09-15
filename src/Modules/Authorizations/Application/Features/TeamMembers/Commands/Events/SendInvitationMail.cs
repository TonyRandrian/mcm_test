using Mcm.Authorizations.Domain.Events;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Services;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.Events
{
    public class SendInvitationMail(
        ICompanyModule companyModule,
        IMailService mailService)
        : INotificationHandler<TeamMemberInvitedEvent>
    {
        private readonly ICompanyModule _companyModule = companyModule;
        private readonly IMailService _mailService = mailService;
        public async Task Handle(TeamMemberInvitedEvent notification, CancellationToken cancellationToken)
        {
            var company = await _companyModule.GetCompanyById(notification.CompanyId)
                ?? throw NotFoundException.NotFoundById("Company", notification.CompanyId);

            await _mailService.SendInvitationAsync(new MailInvitationRequest(
                notification.Identity.LastName,
                notification.Identity.FirstName,
                notification.Identity.Email,
                company.Name,
                notification.Token)
            );
        }
    }
}