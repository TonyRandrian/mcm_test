using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.CreateTypeField
{
    public record CreateTypeFieldRequest
    (
        string Name,
        string Type
    );
}