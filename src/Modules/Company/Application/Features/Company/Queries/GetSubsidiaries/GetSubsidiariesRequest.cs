namespace Mcm.Company.Application.Features.Company.Queries.GetSubsidiaries
{
    public record GetSubsidiariesRequest
    (
        int Page = 1,
        int Limit = 5
    );
}