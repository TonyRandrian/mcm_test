using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.TypeContacts.Queries.GetAllTypeContact
{
    public record GetAllTypeContactQuery(GetAllTypeContactRequest Request)
        : IRequest<ApiResponse<GetAllTypeContactResponse>>;
}