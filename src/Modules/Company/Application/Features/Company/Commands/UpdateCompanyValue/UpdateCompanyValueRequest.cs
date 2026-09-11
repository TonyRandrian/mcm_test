namespace Mcm.Company.Application.Features.Company.Commands.UpdateCompanyValue
{
    public record UpdateCompanyValueRequest
    (
        Guid CategoryId,
        List<CompanyValueRequest> Informations
    );

    public class CompanyValueRequest
    {
        public Guid PropertyId { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}