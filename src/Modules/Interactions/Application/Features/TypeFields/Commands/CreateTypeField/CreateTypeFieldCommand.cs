using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.CreateTypeField
{
    public class CreateTypeFieldCommand
        : IRequest<ApiResponse<CreateTypeFieldResponse>>
    {
        public Guid InteractionTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}