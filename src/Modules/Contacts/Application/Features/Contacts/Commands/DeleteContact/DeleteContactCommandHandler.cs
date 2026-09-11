using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommandHandler(
        IContactRepository contactRepository, 
        IContactUow uow)
        : IRequestHandler<DeleteContactCommand, ApiResponse<DeleteContactResponse>>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IContactUow _uow = uow;

        public async Task<ApiResponse<DeleteContactResponse>> Handle(DeleteContactCommand command, CancellationToken ct)
        {
            var role = await _contactRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Contact), command.Id);
            
            if (command.Force)
                _contactRepository.HardDelete(role);
            else
            {
                role.Delete();
                _contactRepository.Update(role);
            }
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DeleteContactResponse>{
                Success = true,
                Message = "Deleted Contact successfully",
                Code = 200,
                Data = new DeleteContactResponse{Unit = Unit.Value}
            };
        }
    }
}