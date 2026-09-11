using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Contacts.Application.Features.Contacts.Queries.GetContact
{
    public record GetContactQuery(GetContactRequest Request)
        : IRequest<ApiResponse<GetContactResponse>>;
}