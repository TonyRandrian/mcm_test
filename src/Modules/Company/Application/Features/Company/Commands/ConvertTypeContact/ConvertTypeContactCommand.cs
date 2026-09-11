using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.ConvertTypeContact
{
    public class ConvertTypeContactCommand
        : IRequest<ApiResponse<ConvertTypeContactResponse>>
    {
        public Guid Id { get; set; }
    }
}