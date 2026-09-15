using Mcm.Contacts.Application.Interfaces;
using Mcm.Shared.Domain.Events;
using MediatR;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.Events
{
    public class CompanyDeletedEventHandler(
        IContactRepository contactRepository,
        IContactUow uow)
        : INotificationHandler<CompanyDeletedEvent>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IContactUow _uow = uow;
        public async Task Handle(CompanyDeletedEvent notification, CancellationToken cancellationToken)
        {
            var contacts = await _contactRepository.GetAllAsync(
                predicate: c => c.AssociatedCompanyId == notification.CompanyId,
                ct: cancellationToken);

            foreach (var contact in contacts)
                contact.Delete();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}