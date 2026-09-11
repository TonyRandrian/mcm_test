using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.UpdateInteraction
{
    public record UpdateInteractionRequest
    (
        string Title,
        string? Note,
        UpdateInteraction_Date Date,
        UpdateInteraction_Reminder Reminder,
        UpdateInteraction_Participant Participant,
        List<IFormFile> Attachments,
        UpdateInteraction_Information Informations
        
    );

    public record UpdateInteraction_Date(string StartDate, string EndDate);
    public record UpdateInteraction_Reminder(string? Type, double? Value, int? Repeat);
    public record UpdateInteraction_Information(Guid TypeFieldId, string Value);
    public record UpdateInteraction_Participant(List<Guid> Contacts, List<Guid> Members);

}