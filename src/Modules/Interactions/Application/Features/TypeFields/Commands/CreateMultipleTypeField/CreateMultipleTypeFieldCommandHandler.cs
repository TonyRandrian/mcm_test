using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.CreateMultipleTypeField
{
    public class CreateMultipleTypeFieldCommandHandler(
        IInteractionTypeRepository interactionTypeRepository,
        ITypeFieldRepository typeFieldRepository,
        IInteractionUow uow)
        : IRequestHandler<CreateMultipleTypeFieldCommand, ApiResponse<CreateMultipleTypeFieldResponse>>
    {
        private readonly ITypeFieldRepository _typeFieldRepository = typeFieldRepository;
        private readonly IInteractionTypeRepository _interactionTypeRepository = interactionTypeRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<CreateMultipleTypeFieldResponse>> Handle(CreateMultipleTypeFieldCommand command, CancellationToken ct)
        {
            var interactionType = await _interactionTypeRepository.GetByIdAsync(command.InteractionTypeId)
                ?? throw NotFoundException.NotFoundById(nameof(InteractionType), command.InteractionTypeId);

            var fields = command.MultipleField.Select(f => 
                TypeField.Create(
                    interactionType.Id,
                    f.Name,
                    f.Type)
            ).ToList();
            
            await _typeFieldRepository.AddManyAsync(fields);
            await _uow.SaveChangesAsync(ct);
            return new ApiResponse<CreateMultipleTypeFieldResponse>
            {
                Success = true,
                Message = "TypeField created succesfully",
                Code = 200,
                Data = new CreateMultipleTypeFieldResponse{InteractionTypeId = interactionType.Id}
            };
        }
    }
}