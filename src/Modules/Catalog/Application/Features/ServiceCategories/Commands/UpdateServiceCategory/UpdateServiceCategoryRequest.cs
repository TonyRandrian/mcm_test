using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.UpdateServiceCategory
{
    public record UpdateServiceCategoryRequest
    (
        string Name
    );
}