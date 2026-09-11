using Mcm.Company.Application.Interfaces;
using Mcm.Company.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Company.Application.Features.TypeContacts.Commands.CreateTypeContact
{
    public class CreateTypeContactCommandHandler(
        ITypeContactRepository typeContactRepository,
        ICompanyUow uow)
    : IRequestHandler<CreateTypeContactCommand, ApiResponse<CreateTypeContactResponse>>
    {
        private readonly ITypeContactRepository _typeContactRepository = typeContactRepository;
        private readonly ICompanyUow _uow = uow;

        public async Task<ApiResponse<CreateTypeContactResponse>> Handle(CreateTypeContactCommand command, CancellationToken cancellationToken)
        {
            TypeContact? typeContactTo = null;
            if (command.TypeConvertTo is not null)
                typeContactTo = await _typeContactRepository.GetByIdAsync(command.TypeConvertTo.Value)
                    ?? throw NotFoundException.NotFoundById(nameof(TypeContact), command.TypeConvertTo.Value);

            var typeContact = TypeContact.Create(
                command.Name,
                command.Description,
                command.Color,
                typeContactTo?.Id);

            await _typeContactRepository.AddAsync(typeContact);   
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<CreateTypeContactResponse>
            {
                Success = true,
                Message = "Service created successfully",
                Code = 200,
                Data = new CreateTypeContactResponse{TypeContactId = typeContact.Id}
            };
        }
    }
}