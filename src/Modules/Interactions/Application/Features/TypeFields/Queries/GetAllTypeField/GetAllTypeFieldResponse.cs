using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Application.Features.TypeFields.Queries.GetAllTypeField
{
    public class GetAllTypeFieldResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}