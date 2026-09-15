using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetInteractionAttachment
{
    public class GetInteractionAttachmentQueryHandler(
        IInteractionRepository interactionRepository,
        IResourceService resourceService)
        : IRequestHandler<GetInteractionAttachmentQuery, ApiResponse<GetInteractionAttachmentResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IResourceService _resourceService = resourceService;

        public async Task<ApiResponse<GetInteractionAttachmentResponse>> Handle(GetInteractionAttachmentQuery query, CancellationToken cancellationToken)
        {
            var interaction = await _interactionRepository.GetByIdAsync(query.Request.InteractionId)
                ?? throw NotFoundException.NotFoundById(nameof(Interaction), query.Request.InteractionId);
            var resource = interaction.Attachments.FirstOrDefault(a => a.Url == query.Request.FileName)
                ?? throw new NotFoundException("Attachment not found");
            var (stream, contentType, fileName) = _resourceService.OpenResource(resource);

            return new ApiResponse<GetInteractionAttachmentResponse>
            {
                Success = true,
                Message = "Get attachment successfully",
                Code = 200,
                Data = new GetInteractionAttachmentResponse
                {
                    Stream = stream,
                    ContentType = contentType,
                    FileName = fileName
                }
            };
        }
    }
}