using Mcm.Shared.Application.Common;

namespace Mcm.Contacts.Application.Features.Contacts.Queries.GetAllContact
{
    public record GetAllContactRequest
    (
        string? Search = null,
        int Page = 1,
        int Limit = 10,
        DynamicProjection? Projection = null
    );
}