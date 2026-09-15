namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetInteractionAttachment
{
    public class GetInteractionAttachmentResponse
    {
        public Stream Stream { get; set; } = null!;
        public string ContentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}