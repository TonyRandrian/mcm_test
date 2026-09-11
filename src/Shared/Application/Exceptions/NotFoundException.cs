namespace Mcm.Shared.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message = "Entity with id not found"): base(message)
    {
    }

    public static NotFoundException NotFoundByEmail(string email) => new($"Entity with email {email} not found");
    public static NotFoundException NotFoundById(string entity, Guid id) => new($"Entity {entity} with id {id} not found");
}

