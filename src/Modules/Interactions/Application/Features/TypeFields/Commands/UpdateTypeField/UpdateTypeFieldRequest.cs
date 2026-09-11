using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.UpdateTypeField
{
    public record UpdateTypeFieldRequest
    (
        string Name,
        string Type
    );
}