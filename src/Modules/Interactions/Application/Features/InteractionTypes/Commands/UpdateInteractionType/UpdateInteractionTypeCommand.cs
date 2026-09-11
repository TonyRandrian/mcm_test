using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType
{
    public class UpdateInteractionTypeCommand
        : IRequest<ApiResponse<UpdateInteractionTypeResponse>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string LabelColor { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}