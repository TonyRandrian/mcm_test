using Mcm.Company.Application.Features.Company.Commands.CreateCompany;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IResourceService fileService, ICompanyUow uow) : IRequestHandler<UpdateCompanyCommand, ApiResponse<UpdateCompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IResourceService _fileService = fileService;
        private readonly ICompanyUow _uow = uow;

        public async Task<ApiResponse<UpdateCompanyResponse>> Handle(UpdateCompanyCommand command, CancellationToken ct)
        {
            var company = await _companyRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), command.Id);
            
            company.Update(
                command.Name, command.Acronym, command.Description);
            if (command.Logo is not null)
            {
                if (company.Logo is not null)
                    _fileService.DeleteResource(company.Logo);
                company.UpdateLogo(await _fileService.SaveResource(command.Logo));
            }

            company.SynchActivities(command.ActivitySectors ?? []);

            _companyRepository.Update(company);
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<UpdateCompanyResponse>
            {
                Success = true,
                Message = "Company updated successfully",
                Code = 200,
                Data = new UpdateCompanyResponse{ CompanyId = company.Id }
            };
        }
    }
}