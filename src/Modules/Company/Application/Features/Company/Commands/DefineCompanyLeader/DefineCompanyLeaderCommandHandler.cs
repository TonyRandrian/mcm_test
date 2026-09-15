using Mcm.Company.Application.Features.Company.Commands.DeleteCompany;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.DefineCompanyLeader
{
    public class DefineCompanyLeaderHandler(
        ICompanyRepository companyRepository,
        ITeamMemberModule tmModule,
        ICompanyUow uow)
        : IRequestHandler<DefineCompanyLeaderCommand, ApiResponse<DefineCompanyLeaderResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ITeamMemberModule _tmModule = tmModule;
        private readonly ICompanyUow _uow = uow;

        public async Task<ApiResponse<DefineCompanyLeaderResponse>> Handle(DefineCompanyLeaderCommand command, CancellationToken ct)
        {
            var company = await _companyRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), command.Id);
            
            var existUser = await _tmModule.Exists(command.LeaderId);
            if (!existUser)
                throw BadRequestException.Exist("TeamMember");
            company.UpdateLeader(command.LeaderId);

            _companyRepository.Update(company);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DefineCompanyLeaderResponse>
            {
                Success = true,
                Message = "Company deleted successfully",
                Code = 200,
                Data = new DefineCompanyLeaderResponse{ CompanyId = company.Id }
            };
        }
    }
}