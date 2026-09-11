namespace Mcm.Shared.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException() : base("Domain Exception")
        {
        }
        public DomainException(string entity) : base($"Domain Exception: {entity}")
        {    
        }

        public static DomainException GlobalCannotHaveOwner() => new("Global object cannot have owner");  
    }
}