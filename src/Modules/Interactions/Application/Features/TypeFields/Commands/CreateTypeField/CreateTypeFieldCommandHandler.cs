using Mcm.Interactions.Application.Features.InteractionTypes.Commands.CreateInteractionType;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.CreateTypeField
{
    public class CreateTypeFieldCommandHandler(
        IInteractionTypeRepository interactionTypeRepository,
        ITypeFieldRepository typeFieldRepository,
        IInteractionUow uow)
        : IRequestHandler<CreateTypeFieldCommand, ApiResponse<CreateTypeFieldResponse>>
    {
        private readonly ITypeFieldRepository _typeFieldRepository = typeFieldRepository;
        private readonly IInteractionTypeRepository _interactionTypeRepository = interactionTypeRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<CreateTypeFieldResponse>> Handle(CreateTypeFieldCommand command, CancellationToken ct)
        {
            var interactionType = await _interactionTypeRepository.GetByIdAsync(command.InteractionTypeId)
                ?? throw NotFoundException.NotFoundById(nameof(InteractionType), command.InteractionTypeId);
            
            var field = TypeField.Create(
                interactionType.Id,
                command.Name,
                command.Type);

            var exists = await _typeFieldRepository.Validate(
                t => t.Equals(field));
            if (exists is not null)
                throw BadRequestException.Exist(nameof(TypeField));
            
            await _typeFieldRepository.AddAsync(field);
            await _uow.SaveChangesAsync(ct);
            return new ApiResponse<CreateTypeFieldResponse>
            {
                Success = true,
                Message = "TypeField created succesfully",
                Code = 200,
                Data = new CreateTypeFieldResponse{InteractionTypeId = interactionType.Id}
            };
        }
    }
}