using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.DeleteInteraction
{
    public class DeleteInteractionCommand
        : IRequest<ApiResponse<DeleteInteractionResponse>>
    {
       public Guid Id { get; set; }
       public bool Force { get; set; }
    }
}