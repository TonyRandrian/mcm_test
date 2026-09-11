using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler(
        IContactRepository contactRepository,
        ICompanyModule companyModule,
        IPropertyModule propertyModule,
        ICurrentUserService currentUserService,
        IResourceService resourceService,
        IContactUow uow)
        : IRequestHandler<CreateContactCommand, ApiResponse<CreateContactResponse>>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ICompanyModule _companyModule = companyModule;
        private readonly IResourceService _resourceService = resourceService;
        private readonly IPropertyModule _propertyModule = propertyModule;
        private readonly IContactUow _uow = uow;

        public async Task<ApiResponse<CreateContactResponse>> Handle(CreateContactCommand command, CancellationToken ct)
        {
            var company = await _companyModule.GetCompanyById(command.AssociatedCompanyId)
                        ?? throw NotFoundException.NotFoundById("Company", command.AssociatedCompanyId);
            if (!company.IsContact)
                throw new UnauthorizedException("Company is not a contact");

            var contact = Contact.Create(
                new Identity(
                    command.LastName, command.FirstName, command.Email ?? "", command.Poste),
                command.Cin,
                command.Phone,
                company.Id,
                _currentUserService.CompanyId);
            
            if (command.Image is not null)
            {
                var img = await _resourceService.SaveResource(command.Image, FileType.Image);
                contact.UpdateImage(img);
            }

            await _contactRepository.AddAsync(contact);

            foreach (var valueAdd in command.SupplementaryValues)
            {
                var property = await _propertyModule.GetByIdAsync(valueAdd.PropertyId);
                if (property is not null)
                {
                    contact.AddValue(valueAdd.Value, property.Id, property.IsMultiple);
                }       
            }

            await _uow.SaveChangesAsync(ct);
            return new ApiResponse<CreateContactResponse>
            {
                Success = true,
                Message = "Contact created succesfully",
                Code = 200,
                Data = new CreateContactResponse{ContactId = contact.Id}
            };
        }
    }
}