using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Commands.CreateInteractionType
{
    public record CreateInteractionTypeRequest
    (
        string Title,
        string? Description,
        string LabelColor,
        Guid? ParentId
    );
}