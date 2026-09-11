using Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.UpdateFieldValue
{
    public class UpdateFieldValueCommandHandler(
        IInteractionRepository interactionRepository,
        IInteractionUow uow)
        : IRequestHandler<UpdateFieldValueCommand, ApiResponse<UpdateFieldValueResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<UpdateFieldValueResponse>> Handle(UpdateFieldValueCommand command, CancellationToken ct)
        {
            var interaction = await _interactionRepository.GetByIdAsync(command.InteractionId)
                ?? throw NotFoundException.NotFoundById(nameof(Interaction), command.InteractionId); 

            interaction.SyncFieldsValues(
                command.Fields.Select(f => (f.TypeFieldId, f.Value)).ToList());

            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<UpdateFieldValueResponse>
            {
                Success = true,
                Message = "Update Interaction FieldsValue successfulling",
                Code = 200,
                Data = new UpdateFieldValueResponse{ InteractionId = interaction.Id }
            };
        }
    }
}