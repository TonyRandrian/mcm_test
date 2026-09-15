using Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.UpdateInteraction;

public class UpdateInteractionCommand
    : IRequest<ApiResponse<UpdateInteractionResponse>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Note { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string? ReminderType { get; set; } = string.Empty;
    public double? ReminderValue { get; set; }
    public int? ReminderRepeat { get; set; }
    public List<Guid> Contacts { get; set; } = [];
    public List<Guid> TeamMembers { get; set; } = [];
    public List<IFormFile> Attachments { get; set; } = [];
    public IEnumerable<string> OldUrls { get; set; } = [];
    public List<UpdateInteraction_Information>? Informations { get; set; } = [];
}