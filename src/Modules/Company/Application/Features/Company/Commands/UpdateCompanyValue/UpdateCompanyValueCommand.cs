using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.UpdateCompanyValue
{
    public class UpdateCompanyValueCommand
        : IRequest<ApiResponse<UpdateCompanyValueResponse>>
    {
        public Guid CompanyId { get; set; }
        public List<UpdateCompanyValueRequest> Values { get; set; } = [];
    }
}