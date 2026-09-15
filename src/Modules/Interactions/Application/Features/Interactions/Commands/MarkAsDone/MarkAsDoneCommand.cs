using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.MarkAsDone
{
    public class MarkAsDoneCommand
        : IRequest<ApiResponse<MarkAsDoneResponse>>
    {
        public Guid InteractionId { get; set; }
    }
}