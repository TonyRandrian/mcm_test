using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Commands.CreateInteractionType
{
    public class CreateInteractionTypeCommand
        : IRequest<ApiResponse<CreateInteractionTypeResponse>>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string LabelColor { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
    }
}