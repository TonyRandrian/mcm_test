using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<ApiResponse<CreatePropertyResponse>>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsSensitive { get; set; }
    }
}