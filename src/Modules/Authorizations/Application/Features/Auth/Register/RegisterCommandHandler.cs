using System.Globalization;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Events;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Mcm.Authorizations.Application.Features.Auth.Register
{
    public class RegisterCommandHandler(
        ITeamMemberRepository teamMemberRepository,
        ICompanyModule companyModule, 
        IHashPasswordService hashPasswordService, 
        IRoleRepository roleRepository,
        IResourceService resourceService,
        IMediator mediator,
        IAuthorizationUow uow)
        : IRequestHandler<RegisterCommand, ApiResponse<RegisterResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly ICompanyModule _companyModule = companyModule;
        private readonly IHashPasswordService _hashPasswordService = hashPasswordService;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly IMediator _mediator = mediator;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<RegisterResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var company = await _companyModule.CreateAsync(
                command.Name,
                command.Acronym,
                command.Description,
                await _resourceService.SaveResource(command.Logo),
                command.Values?.Select(v => new ValueRequest(v.PropertyId, v.Value)).ToList()
            );

            var exist = await _teamMemberRepository.GetByEmailAsync(new Email(command.Email));
            if (exist is not null)
                throw new BadRequestException($"An account with email '{command.Email}' already exists.");
            
            var adminRole = Role.Create("Administrateur", "Role d'accès total au systeme", isSystem: true);
            adminRole.TenantId = company.TenantId;
            foreach (PermModule module in Enum.GetValues<PermModule>())
                foreach (PermAction action in Enum.GetValues<PermAction>())
                    adminRole.AddPermission(module, action);
            adminRole.AddCompany(company.Id);
            await _roleRepository.AddAsync(adminRole);

            var identity = new Identity(
                command.LastName,
                command.FirstName,
                command.Email,
                "Administrateur");
            var admin = TeamMember.Create(identity, company.Id, tenantId: company.TenantId);
            admin.SetPassword(_hashPasswordService.HashPassword(command.Password));
            admin.InvitationAccepted();

            await _teamMemberRepository.AddAsync(admin);

            adminRole.AddMember(admin.Id);
            await _mediator.Publish(new AdminRegisteredEvent(admin.Id, company.Id), cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
            return new ApiResponse<RegisterResponse>
            {
                Success = true,
                Message = "Administrateur enregistré",
                Code = 200,
                Data = new RegisterResponse
                {
                    TeamMemberId = admin.Id
                }
            };
        }
    }
}