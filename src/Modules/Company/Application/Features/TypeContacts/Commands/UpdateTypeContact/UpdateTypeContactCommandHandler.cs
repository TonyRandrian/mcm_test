using Mcm.Company.Application.Interfaces;
using Mcm.Company.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Company.Application.Features.TypeContacts.Commands.UpdateTypeContact
{
    public class UpdateServiceCategoryCommandHandler(ITypeContactRepository typeContactRepository, ICompanyUow uow)
        : IRequestHandler<UpdateTypeContactCommand, ApiResponse<UpdateTypeContactResponse>>
    {
        private readonly ITypeContactRepository _typeContactRepository = typeContactRepository;
        private readonly ICompanyUow _uow = uow;
        
        public async Task<ApiResponse<UpdateTypeContactResponse>> Handle(UpdateTypeContactCommand command, CancellationToken cancellationToken)
        {
            var typeContact = await _typeContactRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(TypeContact), command.Id);

            typeContact.Update(command.Name, command.Description, command.Color);
    
            _typeContactRepository.Update(typeContact);
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<UpdateTypeContactResponse>
            {
                Success = true,
                Message = "Update Service Category Successfully",
                Code = 200,
                Data = new UpdateTypeContactResponse{TypeContactId = typeContact.Id}
            };
        }
    }
}