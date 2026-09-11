using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommand
        : IRequest<ApiResponse<UpdatePropertyResponse>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        
    }
}