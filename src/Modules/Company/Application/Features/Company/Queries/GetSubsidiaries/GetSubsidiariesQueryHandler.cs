using System.Linq.Expressions;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mcm.Company.Application.Features.Company.Queries.GetSubsidiaries
{
    public class GetSubsidiariesQueryHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetSubsidiariesQuery, ApiResponse<GetSubsidiariesResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<ApiResponse<GetSubsidiariesResponse>> Handle(GetSubsidiariesQuery query, CancellationToken cancellationToken)
        {
            var id = _currentUserService.CompanyId;
            var companies = await _companyRepository.GetAllAsync(
                predicate: e => e.ParentId == id,
                orderBy: e => e.OrderByDescending(c => c.CreatedAt),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit),
                ct: cancellationToken
            );

            return new ApiResponse<GetSubsidiariesResponse>
            {
                Success = true,
                Message = "Subsidiaries retrieved successfully",
                Code = 200,
                Data = new GetSubsidiariesResponse([.. companies.Select(company => new SubsidiariesResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    Acronym = company.Acronym,
                    Description = company.Description,
                    Logo = company.Logo,
                    CreatedAt = company.CreatedAt
                })])
            };

        }
    }
}