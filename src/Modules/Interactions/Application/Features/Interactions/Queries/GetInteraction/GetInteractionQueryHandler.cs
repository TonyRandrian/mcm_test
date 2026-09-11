using Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetInteractionType;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Domain.Extensions;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetInteraction
{
    public class GetInteractionQueryHandler(
        IInteractionRepository interactionRepository,
        ITypeFieldRepository fieldRepository,
        ITeamMemberModule teamMemberModule,
        IContactModule contactModule)
        : IRequestHandler<GetInteractionQuery, ApiResponse<GetInteractionResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly ITypeFieldRepository _fieldRepository = fieldRepository;
        private readonly ITeamMemberModule _teamMemberModule = teamMemberModule;
        private readonly IContactModule _contactModule = contactModule;

        public async Task<ApiResponse<GetInteractionResponse>> Handle(GetInteractionQuery query, CancellationToken cancellationToken)
        {
            var interaction = await _interactionRepository.GetByIdAsync(query.Request.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Interaction), query.Request.Id);

            var fields = await _fieldRepository.GetAllAsync(
                predicate: f =>
                    f.InteractionTypeId == interaction.TypeId
            );

            var fieldsMap = fields
            .Select(field => new
            {
                FieldId = field.Id,
                FieldName = field.Name
            })
            .ToDictionary(
                x => x.FieldId,
                x => new
                {
                    x.FieldId,
                    x.FieldName
                });
            var supplementaryValues = interaction.FieldsValues
            .Select(value =>
                {
                    var field = fieldsMap[value.TypeFieldId];
                    return new Get_Information(
                        field.FieldId,
                        field.FieldName,
                        value.Value
                    );
                })
            .ToList();
            var contacts = await _contactModule.GetIdentities(
                interaction.InteractionContacts.Select(ic => ic.ContactId).ToList());
            var teamMembers = await _teamMemberModule.GetIdentities(
                interaction.InteractionMembers.Select(ic => ic.TeamMemberId).ToList());

            return new ApiResponse<GetInteractionResponse>
            {
                Success = true,
                Message = "Get Interaction successfully",
                Code = 200,
                Data = new GetInteractionResponse
                {
                    Id = interaction.Id,
                    Title = interaction.Title,
                    Note = interaction.Note,
                    Type = new Get_Type(
                        interaction.TypeId,
                        interaction.Type.Title,
                        interaction.Type.LabelColor),
                    Date = new Get_Date(
                        interaction.Date.StartDate.GetDate(),
                        interaction.Date.StartDate.GetHour(),
                        interaction.Date.EndDate.GetDate(),
                        interaction.Date.EndDate.GetHour()),
                    Reminder = new Get_Reminder(
                        interaction.Reminder.Type.ToString(),
                        interaction.Reminder.Value,
                        interaction.Reminder.Repeat),
                    ReportId = interaction.ReportId,
                    Attachments = interaction.Attachments.Select(a => 
                        new Get_Attachment(a.ContentType, a.Url)).ToList(),
                    Participant = new Get_Participant(
                        teamMembers.Select(tm =>
                            new Get_Identity(tm.Id, tm.LastName, tm.FirstName, tm.Image, tm.Email)).ToList(),
                        contacts.Select(c =>
                            new Get_Identity(c.Id, c.LastName, c.FirstName, c.Image, c.Email)).ToList()
                    ),
                    Informations = supplementaryValues
                }
            };
        }
    }
}