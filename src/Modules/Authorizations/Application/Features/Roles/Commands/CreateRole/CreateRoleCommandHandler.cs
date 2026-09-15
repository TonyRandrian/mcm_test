using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler(
        IRoleRepository roleRepository, 
        ICompanyModule companyModule,
        IAuthorizationUow uow)
        : IRequestHandler<CreateRoleCommand, ApiResponse<CreateRoleResponse>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IAuthorizationUow _uow = uow;
        private readonly ICompanyModule _companyModule = companyModule;

        public async Task<ApiResponse<CreateRoleResponse>> Handle(CreateRoleCommand command, CancellationToken ct)
        {
            // Validation Permission Syntaxe
            var parsedPermissions = new List<(PermModule Module, PermAction Action)>();
            foreach (var p in command.Permissions)
            {
                if (!Enum.TryParse<PermModule>(p.Module, ignoreCase: true, out var module))
                    throw new BadRequestException($"Permission Module invalid");

                if (!Enum.TryParse<PermAction>(p.Action, ignoreCase: true, out var action))
                    throw new BadRequestException($"Permission Action invalid");
                parsedPermissions.Add((module, action));
            }
            if (command.Companies.Count > 0)
            {
                var existingCompanies = await _companyModule.GetAllCompanies(command.Companies);
                var existingIds = existingCompanies.Select(c => c.Id).ToHashSet();
                var invalidIds = command.Companies.Where(id => !existingIds.Contains(id)).ToList();
                if (invalidIds.Count > 0)
                    throw new BadRequestException("Company invalid");
            }

            var role = Role.Create(command.Title, command.Description);
            
            // Check same title
            var exists = await _roleRepository.Validate(r =>
                r.Title == role.Title);
            if (exists is not null)
                throw new BadRequestException("Role with same title already exists");

            role.SyncPermissions(parsedPermissions);
            role.SyncCompanies(command.Companies);
            
            await _roleRepository.AddAsync(role);
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<CreateRoleResponse>
            {
                Success = true,
                Message = "Role Created Successfully",
                Code = 200,
                Data = new CreateRoleResponse{ RoleId = role.Id }
            };
        }
    }
}