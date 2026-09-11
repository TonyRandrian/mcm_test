using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Commands.CreateInteractionType
{
    public class CreateInteractionTypeCommandHandler(
        IInteractionTypeRepository interactionTypeRepository,
        IInteractionUow uow)
        : IRequestHandler<CreateInteractionTypeCommand, ApiResponse<CreateInteractionTypeResponse>>
    {
        private readonly IInteractionTypeRepository _interactionTypeRepository = interactionTypeRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<CreateInteractionTypeResponse>> Handle(CreateInteractionTypeCommand command, CancellationToken ct)
        {
            // TO DO : check doubled
            // var exists = await _interactionTypeRepository.Validate() 
            InteractionType? parent = null;
            if (command.ParentId is not null)
                parent = await _interactionTypeRepository.GetByIdAsync(command.ParentId.Value)
                    ?? throw NotFoundException.NotFoundById(nameof(InteractionType), command.ParentId.Value);

            var type = InteractionType.Create(
                command.Title,
                command.LabelColor,
                command.Description,
                parent?.Id);
            
            await _interactionTypeRepository.AddAsync(type);
            await _uow.SaveChangesAsync(ct);
            return new ApiResponse<CreateInteractionTypeResponse>
            {
                Success = true,
                Message = "InteractionType created succesfully",
                Code = 200,
                Data = new CreateInteractionTypeResponse{InteractionTypeId = type.Id}
            };
        }
    }
}