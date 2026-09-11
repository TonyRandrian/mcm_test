using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.DeleteTypeField
{
    public class DeleteTypeFieldCommandHandler(
        ITypeFieldRepository fieldRepository, 
        IInteractionUow uow)
        : IRequestHandler<DeleteTypeFieldCommand, ApiResponse<DeleteTypeFieldResponse>>
    {
        private readonly ITypeFieldRepository _fieldRepository = fieldRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<DeleteTypeFieldResponse>> Handle(DeleteTypeFieldCommand command, CancellationToken ct)
        {
            var field = await _fieldRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(TypeField), command.Id);
            
            if (command.Force)
                _fieldRepository.HardDelete(field);
            else
            {
                field.Delete();
                _fieldRepository.Update(field);
            }
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DeleteTypeFieldResponse>{
                Success = true,
                Message = "Delete TypeField successfully",
                Code = 200,
                Data = new DeleteTypeFieldResponse{Unit = Unit.Value}
            };
        }
    }
}