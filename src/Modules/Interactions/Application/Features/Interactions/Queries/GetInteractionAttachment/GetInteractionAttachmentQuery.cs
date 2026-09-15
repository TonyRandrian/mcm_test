using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetInteractionAttachment
{
    public record GetInteractionAttachmentQuery(GetInteractionAttachmentRequest Request)
    : IRequest<ApiResponse<GetInteractionAttachmentResponse>>;
}