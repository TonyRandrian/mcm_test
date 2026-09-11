using Mcm.Authorizations.Application.Features.Roles.Commands.UpdateRole;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Company.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler(IRoleRepository roleRepository, IAuthorizationUow uow)
        : IRequestHandler<UpdateRoleCommand, ApiResponse<UpdateRoleResponse>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<UpdateRoleResponse>> Handle(UpdateRoleCommand command, CancellationToken ct)
        {
            var role = await _roleRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Role), command.Id);

            role.Update(
                command.Title,
                command.Description
            );

            if (command.Companies is not null)
                role.SyncCompanies(command.Companies);
            if (command.Permissions is not null)
                role.SyncPermissions(command.Permissions.Select(
                    p => (Enum.Parse<PermModule>(p.Module), Enum.Parse<PermAction>(p.Action))));

            _roleRepository.Update(role);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<UpdateRoleResponse>
            {
                Success = true,
                Message = "PATCH UpdateRole",
                Code = 200,
                Data = new UpdateRoleResponse{Id = role.Id}
            };
        }
    }
}