using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.CreateInteraction
{
    public class CreateInteractionCommand
        : IRequest<ApiResponse<CreateInteractionResponse>>
    {
        public string Title { get; set; } = string.Empty;
        public Guid TypeId { get; set; }
        public string? Note { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string? ReminderType { get; set; } = string.Empty;
        public double? ReminderValue { get; set; }
        public int? ReminderRepeat { get; set; }
        public List<Guid>? Contacts { get; set; } = [];
        public List<Guid>? TeamMembers { get; set; } = [];
        public List<CreateInteraction_Information>? Informations { get; set; } = [];
        public List<IFormFile> Attachments { get; set; } = [];
    }

}