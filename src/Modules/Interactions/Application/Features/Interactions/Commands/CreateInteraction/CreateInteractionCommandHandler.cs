using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.CreateInteraction
{
    public class CreateInteractionCommandHandler(
        IInteractionRepository interactionRepository,
        IInteractionTypeRepository typeRepository,
        IResourceService resourceService,
        ICurrentUserService currentUserService,
        IInteractionUow uow)
        : IRequestHandler<CreateInteractionCommand, ApiResponse<CreateInteractionResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IInteractionTypeRepository _typeRepository = typeRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<CreateInteractionResponse>> Handle(CreateInteractionCommand command, CancellationToken ct)
        {
            // TO DO : check doubled
            // var exists = await _interactionTypeRepository.Validate() 

            var type = await _typeRepository.GetByIdAsync(command.TypeId)
                ?? throw NotFoundException.NotFoundById(nameof(InteractionType), command.TypeId);

            var interaction = Interaction.Create(
                command.Title,
                type.Id,
                command.Note,
                command.StartDate,
                command.EndDate,
                command.ReminderType,
                command.ReminderValue,
                command.ReminderRepeat,
                _currentUserService.TeamMemberId);
            
            if (!(await _interactionRepository.IsValidDate(interaction.CreatedBy, interaction.Date.StartDate, interaction.Date.EndDate)))
                throw new BadRequestException("Conflict: Another activity is already scheduled for this date.");

            if (command.Contacts is not null)
                interaction.SyncContact(command.Contacts);
            if (command.TeamMembers is not null)
                interaction.SyncMember(command.TeamMembers);
            
            if (!(await _interactionRepository.IsValidParticipants(
                interaction.Date.StartDate,
                interaction.Date.EndDate,
                interaction.InteractionContacts.Select(ic => ic.ContactId).ToList(),
                interaction.InteractionMembers.Select(ic => ic.TeamMemberId).ToList())))
                throw new BadRequestException("Conflict: participant is already scheduled in other interaction.");

            if (command.Informations is not null)
                interaction.SyncFieldsValues(
                    command.Informations.Select(i => (i.TypeFieldId, i.Value)).ToList());
            
             foreach (var attachment in command.Attachments)
            {
               interaction.AddResource(await _resourceService.SaveResource(attachment, FileType.Document | FileType.Image));
            }

            await _interactionRepository.AddAsync(interaction);
            await _uow.SaveChangesAsync(ct);
            return new ApiResponse<CreateInteractionResponse>
            {
                Success = true,
                Message = "Interaction created succesfully",
                Code = 200,
                Data = new CreateInteractionResponse{ InteractionId = interaction.Id }
            };
        }
    }
}