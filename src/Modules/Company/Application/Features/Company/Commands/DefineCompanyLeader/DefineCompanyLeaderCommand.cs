using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.DefineCompanyLeader
{
    public class DefineCompanyLeaderCommand
        : IRequest<ApiResponse<DefineCompanyLeaderResponse>>
    {
        public Guid Id { get; set; }
        public Guid LeaderId { get; set; }
    }
}