using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.UpdateFieldValue
{
    public record UpdateFieldValueRequest
    (
        Guid TypeFieldId,
        string Value
    );
}