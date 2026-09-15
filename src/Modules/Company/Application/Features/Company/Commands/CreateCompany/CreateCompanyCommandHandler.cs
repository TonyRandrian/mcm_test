using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.Extensions;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler(
        ICompanyRepository companyRepository, 
        IResourceService fileService, 
        ITeamMemberModule tmModule,
        IPropertyModule propertyModule,
        ICurrentUserService currentUserService,
        ITypeContactRepository typeContactRepository,
        ICompanyUow uow)
        : IRequestHandler<CreateCompanyCommand, ApiResponse<CreateCompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IResourceService _fileService = fileService;
        private readonly ITeamMemberModule _tmModule = tmModule;
        private readonly IPropertyModule _propertyModule = propertyModule;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ITypeContactRepository _typeContactRepository = typeContactRepository;
        private readonly ICompanyUow _uow = uow;

        public async Task<ApiResponse<CreateCompanyResponse>> Handle(CreateCompanyCommand command, CancellationToken ct)
        {
            Domain.Entities.Company? parent = null;
            if (command.ParentId is not null)
            {
                parent = await _companyRepository.GetByIdAsync(command.ParentId.Value)
                    ?? throw new BadRequestException("Not null leaderId, not exists");
                if (parent.IsContact != command.IsContact)
                    throw new BadRequestException("Check company type (Organization | Contact)");
            }

            if (command.LeaderId is not null && !(await _tmModule.Exists(command.LeaderId.Value)))
                throw new BadRequestException("Not null leaderId, not exists");

            Domain.Entities.TypeContact? typeContact = null;
            if (command.TypeContactId is not null)
            {
                typeContact = await _typeContactRepository.GetByIdAsync(command.TypeContactId.Value)
                    ?? throw new BadRequestException("Not null TypeContact, not exists");
            }

            var company = Domain.Entities.Company.Create(
                name: command.Name,
                acronym: command.Acronym,
                description: command.Description,
                parentId: parent?.Id,
                isContact: command.IsContact,
                leaderId: command.LeaderId,
                typeContactId: typeContact?.Id,
                companyId: command.IsContact
                    ? _currentUserService.CompanyId
                    : null
            );
            
            var exists = await _companyRepository.Validate(
                c => c.Equals(company));
            if (exists is not null)
                throw BadRequestException.Exist(nameof(Domain.Entities.Company));

            if (command.Logo is not null)
                company.UpdateLogo(await _fileService.SaveResource(command.Logo, FileType.Image));

            company.SynchActivities(command.ActivitySectors ?? []);
        
            if (command.Values is not null)
            {
                var properties = await _propertyModule.GetByIdsAsync(
                    command.Values.Select(v => v.PropertyId).ToList());
                foreach (var v in command.Values)
                {
                    try
                    {
                        v.Value.ConvertTo(properties[v.PropertyId].Type);
                    }
                    catch (FormatException)
                    {   
                        throw;
                    }
                }
                company.AddMultipleValue(command.Values
                    .Select(v => (
                        v.Value, 
                        v.PropertyId,
                        properties[v.PropertyId].IsMultiple, 
                        properties[v.PropertyId].IsSensitive))
                    .ToList());
            }

            await _companyRepository.AddAsync(company);
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<CreateCompanyResponse>
            {
                Success = true,
                Message = "Company created successfully",
                Code = 200,
                Data = new CreateCompanyResponse{ CompanyId = company.Id }
            };
        }
    }
}