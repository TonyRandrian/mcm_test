using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Roles.Queries.GetRole
{
    public class GetRoleQueryHandler(IRoleRepository roleRepository, ICompanyModule companyModule)
        : IRequestHandler<GetRoleQuery, ApiResponse<GetRoleResponse>>
    {
        public IRoleRepository _roleRepository = roleRepository;
        public ICompanyModule _companyModule = companyModule;

        public async Task<ApiResponse<GetRoleResponse>> Handle(GetRoleQuery query, CancellationToken ct)
        {
            var role = await _roleRepository.GetByIdAsync(query.Header.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Role), query.Header.Id);
        
            var companies = await _companyModule.GetAllCompanies(role.RoleCompanies.Select(
                rc => rc.CompanyId).ToList());

            return new ApiResponse<GetRoleResponse>
            {
                Success = true,
                Message = "GET GetRole",
                Code = 200,
                Data = new GetRoleResponse
                {
                    Id = role.Id,
                    Title = role.Title,
                    Description = role.Description,
                    Permissions = [.. role.Authorizations.Select(p => (p.Module.ToString(), p.Action.ToString()))],
                    Companies = [.. companies.Select(c => (c.Id, c.Name))]
                }
            };
        }
    }
}