using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.ConvertTypeContact
{
    public class ConvertTypeContactCommandHandler(
        ICompanyRepository companyRepository,
        ITypeContactRepository typeContactRepository,
        ICompanyUow uow
        )
        : IRequestHandler<ConvertTypeContactCommand, ApiResponse<ConvertTypeContactResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ITypeContactRepository _typeContactRepository = typeContactRepository;
        private readonly ICompanyUow _uow = uow;

        public async Task<ApiResponse<ConvertTypeContactResponse>> Handle(ConvertTypeContactCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), request.Id);

            if (!company.IsContact)
                throw new BadRequestException("Company is not a contact");
            if (company.TypeContact?.TypeConvertTo is null)
                throw new BadRequestException("Cannot convert to this type contact");

            company.ConvertTypeContact();

            _companyRepository.Update(company);
            await _uow.SaveChangesAsync(cancellationToken);
            return new ApiResponse<ConvertTypeContactResponse>
            {
                Success = true,
                Message = "Company Contact converted successfully",
                Code = 200,
                Data = new ConvertTypeContactResponse { CompanyId = company.Id }
            };
        }
    }
}