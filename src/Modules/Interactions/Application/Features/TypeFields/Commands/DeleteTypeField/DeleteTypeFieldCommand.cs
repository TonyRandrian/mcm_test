using System.Text.Json.Serialization;
using Mcm.Interactions.Application.Features.InteractionTypes.Commands.DeleteInteractionType;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.DeleteTypeField
{
    public class DeleteTypeFieldCommand
        : IRequest<ApiResponse<DeleteTypeFieldResponse>>
    {
       public Guid Id { get; set; }
       public bool Force { get; set; }
    }
}