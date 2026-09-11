using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Contacts.Application.Features.Contacts.Queries.GetAllContact
{
    public record GetAllContactQuery(GetAllContactRequest Request)
        : IRequest<ApiResponse<GetAllContactResponse>>
    {
        
    }
}