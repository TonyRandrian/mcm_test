using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetTeamMember
{
    public class GetTeamMemberResponse
    {
        public Guid Id { get; set; }
        public IdentityDto Identity { get; set; } = null!;
        public string Role { get; set; } = null!;
        public Resource? Image { get; set; } = null!;
        public DateTime LastLoginAt { get; set; }
        public List<CategoryTmResponse> SupplementaryData { get; set; } = [];
    }


    public record IdentityDto(
        string LastName,
        string FirstName,
        string FullName,
        string Email,
        string Position
    );
    public class CategoryTmResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<SupplementaryDataTm> Informations { get; set; } = [];
    }
    public class SupplementaryDataTm
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public bool IsSensitive { get; set; }
        public string Value { get; set; } = string.Empty;
    }

}