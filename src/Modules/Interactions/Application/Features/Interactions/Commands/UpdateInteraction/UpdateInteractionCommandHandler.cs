using Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.UpdateInteraction
{
    public class UpdateInteractionCommandHandler(
        IInteractionRepository interactionRepository,
        IResourceService resourceService,
        ITeamMemberModule teamMemberModule,
        IContactModule contactModule,
        IInteractionUow uow)
        : IRequestHandler<UpdateInteractionCommand, ApiResponse<UpdateInteractionResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly ITeamMemberModule _teamMemberModule = teamMemberModule;
        private readonly IContactModule _contactModule = contactModule;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<UpdateInteractionResponse>> Handle(UpdateInteractionCommand command, CancellationToken ct)
        {
            var interaction = await _interactionRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Interaction), command.Id); 

            if (interaction.Date.EndDate < DateTime.UtcNow)
                throw new UnauthorizedException("Passed Interaction Time");
            if (command.Contacts.Count > 0)
            {
                var existingContacts = await _contactModule.GetIdentities(command.Contacts);
                var missingContacts = command.Contacts
                    .Except(existingContacts.Select(c => c.Id))
                    .ToList();
                if (missingContacts.Count > 0)
                    throw new BadRequestException("Contact not found");
            }
            if (command.TeamMembers.Count > 0)
            {
                var existingMembers = await _teamMemberModule.GetIdentities(command.TeamMembers);
                var missingMembers = command.TeamMembers
                    .Except(existingMembers.Select(m => m.Id))
                    .ToList();
                if (missingMembers.Count > 0)
                    throw new BadRequestException("TeamMember not found");
            }

            interaction.Update(
                command.Title,
                command.Note,
                command.StartDate,
                command.EndDate,
                command.ReminderType,
                command.ReminderValue,
                command.ReminderRepeat);

            interaction.SyncContact(command.Contacts);
            interaction.SyncMember(command.TeamMembers);

            List<Resource> attachments = [];
            foreach (var attachment in command.Attachments)
                attachments.Add(await _resourceService.SaveResource(attachment, FileType.Image & FileType.Document));
            var toRemove = interaction.SyncAttachments(
                command.OldUrls, attachments);
            foreach (var attachment in toRemove)
                _resourceService.DeleteResource(attachment);

            foreach (var (typeFieldId, value) in command.Informations ?? [])
                interaction.UpdateFieldValue(typeFieldId, value);

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