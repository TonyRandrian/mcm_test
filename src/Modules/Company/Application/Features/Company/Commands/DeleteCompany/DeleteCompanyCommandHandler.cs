using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler(ICompanyRepository companyRepository, IResourceService fileService, ICompanyUow uow)
        : IRequestHandler<DeleteCompanyCommand, ApiResponse<DeleteCompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IResourceService _fileService = fileService;
        private readonly ICompanyUow _uow = uow;

        public async Task<ApiResponse<DeleteCompanyResponse>> Handle(DeleteCompanyCommand command, CancellationToken ct)
        {
            var company = await _companyRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), command.Id);
            
            if (command.Force)
            {
                if (company.Logo is not null)
                    _fileService.DeleteResource(company.Logo);
                _companyRepository.HardDelete(company);
            }
            else
            {
                company.Delete();
                _companyRepository.Update(company);
            }

            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DeleteCompanyResponse>
            {
                Success = true,
                Message = "Company deleted successfully",
                Code = 200,
                Data = new DeleteCompanyResponse(true)
            };
        }
    }
}