namespace Mcm.Company.Application.Features.Company.Commands.AddCompanyValue
{
    public record AddCompanyValueRequestHeader
    (
        Guid CompanyId
    );
    
    public class AddCompanyValueRequestBody
    {
        public List<CompanyValueAdd> Values { get; set; } = [];
    }

    public class CompanyValueAdd
    {
        public string Value { get; set; } = string.Empty;
        public Guid PropertyId { get; set; }
    }
}