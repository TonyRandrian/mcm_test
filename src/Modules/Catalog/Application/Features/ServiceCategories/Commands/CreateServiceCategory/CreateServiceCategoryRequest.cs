using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public record CreateServiceCategoryRequest
    (
        Guid? ParentCategoryId,
        string Name
    );
}