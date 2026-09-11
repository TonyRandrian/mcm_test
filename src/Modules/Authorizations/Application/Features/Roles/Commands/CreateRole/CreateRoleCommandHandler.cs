using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler(IRoleRepository roleRepository, IAuthorizationUow uow)
        : IRequestHandler<CreateRoleCommand, ApiResponse<CreateRoleResponse>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<CreateRoleResponse>> Handle(CreateRoleCommand command, CancellationToken ct)
        {
            var role = Role.Create(command.Title, command.Description);
            

            role.SyncPermissions(
                command.Permissions.Select(p => (
                    Enum.Parse<PermModule>(p.Module), Enum.Parse<PermAction>(p.Action))));
            role.SyncCompanies(command.Companies);
            
            await _roleRepository.AddAsync(role);
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<CreateRoleResponse>
            {
                Success = true,
                Message = "POST CreateRole",
                Code = 200,
                Data = new CreateRoleResponse{ RoleId = role.Id }
            };
        }
    }
}