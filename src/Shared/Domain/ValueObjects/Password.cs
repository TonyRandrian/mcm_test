using System.Text.RegularExpressions;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Shared.Domain.ValueObjects
{
    public class Password
        : ValueObject
    {
        public string Value { get; private set; } = string.Empty;

        public Password(string value)
        {
            if (string.IsNullOrEmpty(value) || !IsValid(value))
                throw new ArgumentException("Password argument invalid", nameof(value));
            Value = value;
        }

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return Value;
        }

        public static bool IsValid(string value)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}";
            return Regex.IsMatch(value, pattern);
        }

        public static bool TryParse(string value, out string password)
        {
            try
            {
                Password pwd = new(value);
                password = pwd.Value;
                return true;
            }
            catch (System.Exception)
            {
                password = "";
                return false;
            }
        }

        public static implicit operator string(Password email) =>  email.Value;
        public static implicit operator Password(string value) =>  new(value);
    }
}