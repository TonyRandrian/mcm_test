using Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.UpdateTypeField
{
    public class UpdateTypeFieldCommand
        : IRequest<ApiResponse<UpdateTypeFieldResponse>>
    {
        public Guid TypeFieldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}