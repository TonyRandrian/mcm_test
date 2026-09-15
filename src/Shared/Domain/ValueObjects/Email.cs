using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Shared.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; private set; } = string.Empty;


        public Email(string value)
        {
            if (string.IsNullOrEmpty(value) || !(new EmailAddressAttribute().IsValid(value)))
                throw new ArgumentException("Email argument invalid", nameof(value));
            Value = value.ToLowerInvariant();
        }

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return Value;
        }

        public static bool TryParse(string value, out string emailValue)
        {
            try
            {
                Email email = new Email(value);
                emailValue = email.Value;
                return true;
            }
            catch (System.Exception)
            {
                emailValue = "";
                return false;
            }
        }

        public static implicit operator string(Email email) =>  email.Value;
        public static implicit operator Email(string value) =>  new Email(value);
    }
}