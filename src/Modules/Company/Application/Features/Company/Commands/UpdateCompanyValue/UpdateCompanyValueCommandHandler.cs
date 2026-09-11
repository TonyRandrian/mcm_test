using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Commands.UpdateCompanyValue
{
    public class UpdateCompanyValueCommandHandler(
        ICurrentUserService currentUserService, ICompanyRepository companyRepository, ICategoryModule categoryModule, ICompanyUow uow)
        : IRequestHandler<UpdateCompanyValueCommand, ApiResponse<UpdateCompanyValueResponse>>
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ICategoryModule _categoryModule = categoryModule;
        private readonly ICompanyUow _uow = uow;
        public async Task<ApiResponse<UpdateCompanyValueResponse>> Handle(UpdateCompanyValueCommand command, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.FindCompanyByIdOutTenant(command.CompanyId)
                ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), command.CompanyId);
            
            foreach (var valueRequest in command.Values)
            {
                var category = await _categoryModule.GetByIdAsync(valueRequest.CategoryId)
                    ?? throw NotFoundException.NotFoundById("Category", valueRequest.CategoryId);

                var propertyMap = category.Properties.ToDictionary(p => p.Id);

                var validRequests = valueRequest.Informations
                    .Where(i => propertyMap.ContainsKey(i.PropertyId))
                    .ToList();
                var requestedPropertyIds = validRequests.Select(i => i.PropertyId).ToHashSet();

                foreach (var property in category.Properties)
                {
                    if (!requestedPropertyIds.Contains(property.Id))
                    {
                        company.RemoveAllValuesByProperty(property.Id);
                    }
                }

                var grouped = validRequests.GroupBy(i => i.PropertyId);
                foreach (var group in grouped)
                {
                    var propertyId = group.Key;
                    var property = propertyMap[propertyId];

                    if (!property.IsMultiple)
                    {
                        company.UpdateValue(group.First().Value, propertyId);
                    }
                    else
                    {
                        company.RemoveAllValuesByProperty(propertyId);
                        foreach (var item in group)
                        {
                            company.AddValue(item.Value, propertyId, isMultiple: true);
                        }
                    }
                }
            }

            await _uow.SaveChangesAsync(cancellationToken);

            return new ApiResponse<UpdateCompanyValueResponse>
            {
                Success = true,
                Message = "Company value updated successfully",
                Code = 200,
                Data = new UpdateCompanyValueResponse{CompanyId = company.Id}
            };
        }
    }
}