using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.MarkAsDoneMany
{
    public class MarkAsDoneManyCommand
        : IRequest<ApiResponse<MarkAsDoneManyResponse>>
    {
        public List<Guid> InteractionIds { get; set; } = [];
    }
}