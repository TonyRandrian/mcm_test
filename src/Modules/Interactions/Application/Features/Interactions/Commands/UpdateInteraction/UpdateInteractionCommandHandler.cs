using Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.UpdateInteraction
{
    public class UpdateInteractionCommandHandler(
        IInteractionRepository interactionRepository,
        IResourceService resourceService,
        IInteractionUow uow)
        : IRequestHandler<UpdateInteractionCommand, ApiResponse<UpdateInteractionResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<UpdateInteractionResponse>> Handle(UpdateInteractionCommand command, CancellationToken ct)
        {
            var interaction = await _interactionRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Interaction), command.Id); 

            interaction.Update(
                command.Title,
                command.TypeId,
                command.Note,
                command.StartDate,
                command.EndDate,
                command.ReminderType,
                command.ReminderValue,
                command.ReminderRepeat);

            interaction.SyncContact(command.Contacts);
            interaction.SyncMember(command.TeamMembers);

            // TODO: Synchronize Attachments
            // foreach (var attachment in command.Attachments)
            // {
            //     var file = await _resourceService.SaveResource(attachment, FileType.Image & FileType.Document);
            //     interaction.;
            // }

            _interactionRepository.Update(interaction);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<UpdateInteractionResponse>
            {
                Success = true,
                Message = "Update Interaction successfulling",
                Code = 200,
                Data = new UpdateInteractionResponse{ InteractionId = interaction.Id }
            };
        }
    }
}