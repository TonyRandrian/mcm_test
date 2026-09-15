using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyContacts
{
    public class GetCompanyContactsQueryHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetCompanyContactsQuery, ApiResponse<GetCompanyContactsResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        

        public async Task<ApiResponse<GetCompanyContactsResponse>> Handle(GetCompanyContactsQuery query, CancellationToken cancellationToken)
        {
            var id = _currentUserService.CompanyId;
            var companies = await _companyRepository.GetAllAsync(
                predicate: e => e.IsContact,
                orderBy: e => e.OrderByDescending(e => e.CreatedAt),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit),
                ct: cancellationToken
            );

            return new ApiResponse<GetCompanyContactsResponse>
            {
                Success = true,
                Message = "Company contacts retrieved successfully",
                Code = 200,
                Data = new GetCompanyContactsResponse([.. companies.Select(company => new CompanyContactDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    Acronym = company.Acronym,
                    Description = company.Description,
                    Logo = company.Logo,
                    CreatedAt = company.CreatedAt
                })]),
                Meta = new Meta
                {
                    Page = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _companyRepository.CountAsync(predicate: e => e.IsContact)
                }
            };
        }
    }
}