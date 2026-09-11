using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.TypeContacts.Commands.DeleteTypeContact
{
    public class DeleteTypeContactCommand
        : IRequest<ApiResponse<DeleteTypeContactResponse>>
    {
        public Guid Id { get; set; }
        public bool Force { get; set; } = false;
    }
}