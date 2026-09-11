using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.AddCompanyValue
{
    public class AddCompanyValueCommand : IRequest<ApiResponse<AddCompanyValueResponse>>
    {
        public AddCompanyValueRequestHeader Header { get; set; } = null!;
        public AddCompanyValueRequestBody Body { get; set; } = null!;
    }
}