using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Authorizations.Domain.Extensions;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InviteTeamMember
{
    public class InviteTeamMemberCommandHandler(
        ITeamMemberRepository teamMemberRepository, 
        ICompanyModule companyModule, 
        IRoleRepository roleRepository,
        IJwtTokenService jwtTokenService, 
        IAuthorizationUow uow)
        : IRequestHandler<InviteTeamMemberCommand, ApiResponse<InviteTeamMemberResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly ICompanyModule _companyModule = companyModule;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<InviteTeamMemberResponse>> Handle(InviteTeamMemberCommand command, CancellationToken ct)
        {
            var company = await _companyModule.GetCompanyById(command.CompanyId)
                ?? throw NotFoundException.NotFoundById("Company", command.CompanyId);
            Role? role = null;
            if (!command.IsLeader)
            {
                if (command.RoleId is null)
                    throw new BadRequestException("RoleId must be entered in context");
                role = await _roleRepository.GetByIdAsync(command.RoleId.Value)
                    ?? throw NotFoundException.NotFoundById(nameof(Role), command.RoleId.Value);
            }
            else if (! await _companyModule.HasLeader(company.Id))
            {
                role = Role.Create("Dirigeant", $"Role dirigeant de l'entreprise {company.Name}", true);
                role.AddManyPermission(LeaderPermission.GetValues());
                role.AddCompany(company.Id);
                await _roleRepository.AddAsync(role);
            }
            else
            {
                throw new BadRequestException("Leader already exists in company");
            }

            var teamMember = TeamMember.Create(
                new Identity(
                    command.FirstName ?? string.Empty, command.LastName ?? string.Empty, command.Email, string.Empty),
                company.Id);
            var exists = await _teamMemberRepository.GetByEmailAsync(teamMember.Identity.Email);
            if (exists is not null)
            {
                throw BadRequestException.Exist(nameof(TeamMember));
            }
            
            var token = await _jwtTokenService.GenerateInvitationToken(teamMember.Id, company.Id);
            teamMember.SetInvitationToken(token);

            await _teamMemberRepository.AddAsync(teamMember);
            role.AddMember(teamMember.Id);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<InviteTeamMemberResponse>
            {
                Success = true,
                Message = "Invitation TeamMember succesfully",
                Code = 200,
                Data = new InviteTeamMemberResponse{Token = token}
            };
        }
    }
}