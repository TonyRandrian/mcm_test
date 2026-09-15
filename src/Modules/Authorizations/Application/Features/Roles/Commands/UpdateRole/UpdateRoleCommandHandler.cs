using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler(
        IRoleRepository roleRepository, 
        ICompanyModule companyModule,
        IAuthorizationUow uow)
        : IRequestHandler<UpdateRoleCommand, ApiResponse<UpdateRoleResponse>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly ICompanyModule _companyModule = companyModule;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<UpdateRoleResponse>> Handle(UpdateRoleCommand command, CancellationToken ct)
        {
            var role = await _roleRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Role), command.Id);

            role.Update(
                command.Title,
                command.Description
            );

            // Validation Permission Syntaxe
            var parsedPermissions = new List<(PermModule Module, PermAction Action)>();
            if (command.Permissions is not null)
            {
                foreach (var p in command.Permissions)
                {
                    if (!Enum.TryParse<PermModule>(p.Module, ignoreCase: true, out var module))
                        throw new BadRequestException($"Permission Module invalid");
                    if (!Enum.TryParse<PermAction>(p.Action, ignoreCase: true, out var action))
                        throw new BadRequestException($"Permission Action invalid");
                    parsedPermissions.Add((module, action));
                }
                role.SyncPermissions(parsedPermissions);
            }
            
            if (command.Companies is not null && command.Companies.Count > 0)
            {
                var existingCompanies = await _companyModule.GetAllCompanies(command.Companies);
                var existingIds = existingCompanies.Select(c => c.Id).ToHashSet();
                var invalidIds = command.Companies.Where(id => !existingIds.Contains(id)).ToList();
                if (invalidIds.Count > 0)
                    throw new BadRequestException("Company invalid");
                role.SyncCompanies(command.Companies);
            }
            
            _roleRepository.Update(role);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<UpdateRoleResponse>
            {
                Success = true,
                Message = "Role updated successfully",
                Code = 200,
                Data = new UpdateRoleResponse{Id = role.Id}
            };
        }
    }
}