using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.Services.Commands.DeleteService
{
    public class DeleteServiceCommand
        : IRequest<ApiResponse<DeleteServiceResponse>>
    {
        public Guid Id { get; set; }
        public bool Force { get; set; } = false;
    }
}