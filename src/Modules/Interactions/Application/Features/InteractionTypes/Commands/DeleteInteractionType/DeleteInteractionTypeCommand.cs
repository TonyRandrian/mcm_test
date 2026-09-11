using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Commands.DeleteInteractionType
{
    public class DeleteInteractionTypeCommand
        : IRequest<ApiResponse<DeleteInteractionTypeResponse>>
    {
       public Guid Id { get; set; }
       public bool Force { get; set; }
    }
}