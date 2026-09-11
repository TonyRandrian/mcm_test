namespace Mcm.Contacts.Application.Features.Contacts.Commands.DeleteContact
{
    public record DeleteContactRequest
    (
        bool Force = false
    );
}