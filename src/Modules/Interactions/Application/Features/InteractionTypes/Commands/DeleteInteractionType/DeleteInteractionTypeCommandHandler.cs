using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Commands.DeleteInteractionType
{
    public class DeleteInteractionTypeCommandHandler(
        IInteractionTypeRepository typeRepository, 
        IInteractionRepository interactionRepository, 
        IInteractionUow uow)
        : IRequestHandler<DeleteInteractionTypeCommand, ApiResponse<DeleteInteractionTypeResponse>>
    {
        private readonly IInteractionTypeRepository _interactionTypeRepository = typeRepository;
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<DeleteInteractionTypeResponse>> Handle(DeleteInteractionTypeCommand command, CancellationToken ct)
        {
            var type = await _interactionTypeRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(InteractionType), command.Id);

            var interactions = await _interactionRepository.GetAllAsync(
                predicate: i =>
                    i.TypeId == type.Id,
                ct: ct);
            if (interactions.Any())
                throw new BadRequestException("Has interaction saved in type");

            if (command.Force)
                _interactionTypeRepository.HardDelete(type);
            else
            {
                type.Delete();
                _interactionTypeRepository.Update(type);
            }
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DeleteInteractionTypeResponse>{
                Success = true,
                Message = "Deleted InteractionType successfully",
                Code = 200,
                Data = new DeleteInteractionTypeResponse{Unit = Unit.Value}
            };
        }
    }
}