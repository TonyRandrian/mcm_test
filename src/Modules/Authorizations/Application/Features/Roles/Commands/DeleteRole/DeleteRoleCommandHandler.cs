using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler(IRoleRepository roleRepository, IAuthorizationUow uow)
        : IRequestHandler<DeleteRoleCommand, ApiResponse<DeleteRoleResponse>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<DeleteRoleResponse>> Handle(DeleteRoleCommand command, CancellationToken ct)
        {
            var role = await _roleRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Role), command.Id);
            
            if (role.IsSystem)
                throw new UnauthorizedException("System Role cannot be deleted");

            if (command.Force)
                _roleRepository.HardDelete(role);
            else 
                role.Delete();
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DeleteRoleResponse>{
                Success = true,
                Message = "DELETE DeleteRole",
                Code = 200,
                Data = new DeleteRoleResponse{Unit = Unit.Value}
            };
        }
    }
}