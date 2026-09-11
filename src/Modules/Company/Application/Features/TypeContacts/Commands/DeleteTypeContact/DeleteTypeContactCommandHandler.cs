using Mcm.Company.Application.Interfaces;
using Mcm.Company.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Company.Application.Features.TypeContacts.Commands.DeleteTypeContact
{
    public class DeleteTypeContactCommandHandler(ITypeContactRepository typeContactRepository, ICompanyUow uow)
    : IRequestHandler<DeleteTypeContactCommand, ApiResponse<DeleteTypeContactResponse>>
    {
        private readonly ITypeContactRepository _serviceCategoryRepository = typeContactRepository;
        private readonly ICompanyUow _uow = uow;

        public async Task<ApiResponse<DeleteTypeContactResponse>> Handle(DeleteTypeContactCommand command, CancellationToken cancellationToken)
        {
            var typeContact = await _serviceCategoryRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(TypeContact), command.Id);

            if (command.Force)
                _serviceCategoryRepository.HardDelete(typeContact);
            else
            {
                typeContact.Delete();
                _serviceCategoryRepository.Update(typeContact);
            }
        
            await _uow.SaveChangesAsync(cancellationToken);    
            return new ApiResponse<DeleteTypeContactResponse>
            {
                Success = true,
                Message = "Delete TypeContact Successfully",
                Code = 200,
                Data = new DeleteTypeContactResponse{Unit = Unit.Value}
            };
        }
    }
}