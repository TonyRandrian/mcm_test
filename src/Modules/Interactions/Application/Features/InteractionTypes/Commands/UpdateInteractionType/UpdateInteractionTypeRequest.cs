using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType
{
    public record UpdateInteractionTypeRequest
    (
        string Title,
        string LabelColor,
        string? Description
    );
}