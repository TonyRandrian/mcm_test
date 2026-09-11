using Mcm.Company.Application.Features.Company.Queries.GetSubsidiaries;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mcm.Company.Application.Features.Company.Queries.GetSubsidiariesById
{
    public class GetSubsidiariesByIdQueryHandler(ICompanyRepository companyRepository)
        : IRequestHandler<GetSubsidiariesByIdQuery, ApiResponse<GetSubsidiariesByIdResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;

        public async Task<ApiResponse<GetSubsidiariesByIdResponse>> Handle(GetSubsidiariesByIdQuery query, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllAsync(
                predicate: (e => e.ParentId == query.CompanyId), 
                orderBy: (e => e.OrderByDescending(c => c.CreatedAt)), 
                pageQuery: new PageQuery(query.Page, query.Limit)
                , ct: cancellationToken);

            return new ApiResponse<GetSubsidiariesByIdResponse>
            {
                Success = true,
                Message = "Subsidiaries retrieved successfully",
                Code = 200,
                Data = new GetSubsidiariesByIdResponse(companies.Select(company => new SubsidiariesByIdResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    Acronym = company.Acronym,
                    Description = company.Description,
                    Logo = company.Logo,
                    CreatedAt = company.CreatedAt
                }).ToList())
            };

        }
    }
}