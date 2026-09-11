using Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.UpdateFieldValue;

public class UpdateFieldValueCommand
    : IRequest<ApiResponse<UpdateFieldValueResponse>>
{
    public Guid InteractionId { get; set; }
    public List<UpdateFieldValueRequest> Fields { get; set; } = new();
}