using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommand
        : IRequest<ApiResponse<DeletePropertyResponse>>
    {
        public Guid Id { get; set; }
        public bool Force { get; set; }
    }
}