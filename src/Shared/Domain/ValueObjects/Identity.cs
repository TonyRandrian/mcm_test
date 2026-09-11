using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Shared.Domain.ValueObjects
{
    public class Identity : ValueObject
    {
        public Name LastName { get; set; } = null!;
        public Name FirstName { get; set; } = null!;
        public Email Email { get; set; } = null!;
        public string Position
        {
            get;
            set => field = value.ToCapitalize(); 
        } = string.Empty;
        public string FullName
            => $"{FirstName.Value} {LastName.Value}".Trim();

        private Identity(){}
        public Identity(string lastName, string firstName, string email, string position)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = new Email(email);
            Position = position;
        }
        public Identity(string email, string position = "")
            :this(string.Empty, string.Empty, email, position)
        {}

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return LastName.Value;
            yield return FirstName.Value;
            yield return Email.Value;
            yield return Position;
        }
    }
}