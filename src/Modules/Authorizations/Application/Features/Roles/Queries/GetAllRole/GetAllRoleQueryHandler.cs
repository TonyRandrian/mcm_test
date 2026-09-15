using System.Linq.Expressions;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mcm.Authorizations.Application.Features.Roles.Queries.GetAllRole
{
    public class GetAllRoleQueryHandler(IRoleRepository roleRepository)
        : IRequestHandler<GetAllRoleQuery, ApiResponse<GetAllRoleResponse>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<ApiResponse<GetAllRoleResponse>> Handle(GetAllRoleQuery query, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetAllAsync(
                includes: [role => role.Members],
                pageQuery: new PageQuery(query.Header.Page, query.Header.Limit)
            );
        
            return new ApiResponse<GetAllRoleResponse>
            {
                Success = true,
                Message = "GET GetAllRole",
                Code = 200,
                Data = new GetAllRoleResponse
                {
                    Roles = [.. role.Select(role => new RoleResponse
                        {
                            Id = role.Id,
                            Title = role.Title,
                            Description = role.Description,
                            Count = role.Members.Count,
                        }
                    )]
                },
                Meta = new Meta
                {
                    Page = query.Header.Page,
                    Limit = query.Header.Limit,
                    Total = await _roleRepository.CountAsync()
                }
            };
        }
    }
}