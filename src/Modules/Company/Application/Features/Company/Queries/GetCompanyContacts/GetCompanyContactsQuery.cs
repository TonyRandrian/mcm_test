using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyContacts
{
    public record GetCompanyContactsQuery(GetCompanyContactsRequest Request)
        : IRequest<ApiResponse<GetCompanyContactsResponse>>;
}