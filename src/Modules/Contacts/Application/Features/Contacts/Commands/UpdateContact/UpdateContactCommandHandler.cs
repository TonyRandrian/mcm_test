using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandHandler(
        IContactRepository contactRepository,
        IResourceService fileService,
        IContactUow uow)
        : IRequestHandler<UpdateContactCommand, ApiResponse<UpdateContactResponse>>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IResourceService _fileService = fileService;
        private readonly IContactUow _uow = uow;

        public async Task<ApiResponse<UpdateContactResponse>> Handle(UpdateContactCommand command, CancellationToken ct)
        {
            var contact = await _contactRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Contact), command.Id); 

            if (command.Image is not null)
            {
                if (contact.Image is not null)
                    _fileService.DeleteResource(contact.Image);
                var image = await _fileService.SaveResource(command.Image);
                contact.UpdateImage(image);
            }
            contact.Update(
                new Identity(
                    command.LastName ?? contact.Identity.LastName.Value,
                    command.FirstName ?? contact.Identity.FirstName.Value, 
                    command.Email ?? contact.Identity.Email.Value, 
                    command.Poste ?? contact.Identity.Position), 
                command.Cin, command.Phone, 
                command.AssociatedCompanyId);

            _contactRepository.Update(contact);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<UpdateContactResponse>
            {
                Success = true,
                Message = "POST CreateTeamMember",
                Code = 200,
                Data = new UpdateContactResponse{ContactId = contact.Id}
            };
        }
    }
}