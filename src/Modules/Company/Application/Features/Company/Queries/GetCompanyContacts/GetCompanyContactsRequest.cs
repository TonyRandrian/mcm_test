using Mcm.Shared.Application.Common;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyContacts
{
    public class GetCompanyContactsRequest
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
        public DynamicProjection? Projection { get; set; }

    }
}