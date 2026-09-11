using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.DeleteInteraction
{
    public class DeleteInteractionCommandHandler(
        IInteractionRepository typeRepository, 
        IInteractionUow uow)
        : IRequestHandler<DeleteInteractionCommand, ApiResponse<DeleteInteractionResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = typeRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<DeleteInteractionResponse>> Handle(DeleteInteractionCommand command, CancellationToken ct)
        {
            var interaction = await _interactionRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Interaction), command.Id);
            
            if (command.Force)
                _interactionRepository.HardDelete(interaction);
            else
            {
                interaction.Delete();
                _interactionRepository.Update(interaction);
            }
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DeleteInteractionResponse>{
                Success = true,
                Message = "Delete Interaction successfully",
                Code = 200,
                Data = new DeleteInteractionResponse{Unit = Unit.Value}
            };
        }
    }
}