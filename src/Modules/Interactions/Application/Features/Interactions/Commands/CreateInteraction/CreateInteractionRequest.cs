using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.CreateInteraction
{
    public record CreateInteractionRequest
    (
        string Title,
        Guid TypeId,
        string? Note,
        CreateInteraction_Date Date,
        CreateInteraction_Reminder Reminder,
        CreateInteraction_Participant Participant,
        string? Informations,
        List<IFormFile> Attachments
    );

    public record CreateInteraction_Date(string StartDate, string EndDate);
    public record CreateInteraction_Reminder(string? Type, double? Value, int? Repeat);
    public record CreateInteraction_Participant(List<Guid>? Members, List<Guid>? Contacts);
    public record CreateInteraction_Information(Guid TypeFieldId, string Value);
}