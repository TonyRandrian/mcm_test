namespace Mcm.Company.Application.Features.Company.Commands.DeleteCompany
{
    public record DeleteCompanyRequest
    (
        bool Force = false
    );
}