using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Domain.Events;
using MediatR;

namespace Mcm.Authorizations.Application.Features.EventHandler
{
    public class AddAdminSubsidiary(
        IRoleRepository roleRepository,
        IAuthorizationUow uow)
        : INotificationHandler<SubsidiaryCreatedEvent>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IAuthorizationUow _uow = uow;

        public async Task Handle(SubsidiaryCreatedEvent notification, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetAdminAsync()
                ?? throw new NotFoundException("Admin Role not found");
            role.AddCompany(notification.CompanyId);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}