using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdateTmValue
{
    public class UpdateTmValueCommand
        : IRequest<ApiResponse<UpdateTmValueResponse>>
    {
        public Guid TeamMemberId { get; set; }
        public List<UpdateTmValueRequest> Values { get; set; } = [];
    }
}