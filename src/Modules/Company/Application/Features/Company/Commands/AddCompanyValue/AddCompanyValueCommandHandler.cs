using Mcm.Company.Application.Features.Company.Queries.GetCompanyById;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.AddCompanyValue
{
        public class AddCompanyValueCommandHandler(ICompanyRepository companyRepository, IPropertyModule propertyModule, ICompanyUow uow) : IRequestHandler<AddCompanyValueCommand, ApiResponse<AddCompanyValueResponse>>
        {
            private readonly ICompanyRepository _companyRepository = companyRepository;
            private readonly IPropertyModule _propertyModule = propertyModule;
            private readonly ICompanyUow _uow = uow;

            public async Task<ApiResponse<AddCompanyValueResponse>> Handle(AddCompanyValueCommand command, CancellationToken ct)
            {
                var company = await _companyRepository.FindCompanyByIdOutTenant(command.Header.CompanyId)
                            ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), command.Header.CompanyId);

                foreach (var value in command.Body.Values)
                {
                    var property = await _propertyModule.GetByIdAsync(value.PropertyId);
                    if (property is not null)
                    {
                        company.AddValue(value.Value, property.Id, property.IsMultiple, property.IsSensitive);
                    }
                    
                }
                
                await _uow.SaveChangesAsync(ct);
                return new ApiResponse<AddCompanyValueResponse>
                {
                    Success = true,
                    Message = "Company value added successfully",
                    Code = 200,
                    Data = new AddCompanyValueResponse{CompanyId = company.Id}
                };
            }
    }
}