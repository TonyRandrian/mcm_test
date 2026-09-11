namespace Mcm.Company.Application.Features.TypeContacts.Queries.GetAllTypeContact
{
    public record GetAllTypeContactRequest
    (
        string? SearchName = null, 
        int Page = 1, 
        int Limit = 5
    );
}