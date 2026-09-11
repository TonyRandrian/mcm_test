namespace Mcm.Company.Application.Features.Company.Queries.GetSubsidiariesById
{
    public record GetSubsidiariesByIdRequest
    (
        int Page = 1,
        int Limit = 5
    );
}