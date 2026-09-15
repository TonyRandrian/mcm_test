using System.Text.RegularExpressions;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Shared.Domain.ValueObjects
{
    public class PhoneNumber
        : ValueObject
    {
        public string Value { get; private set; } = string.Empty;


        public PhoneNumber(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || !IsValid(value))
                throw new ArgumentException("PhoneNumber argument invalid", nameof(value));
            Value = value.Trim();
        }

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return Value;
        }

        public static bool IsValid(string value){
            string pattern = @"^[\+][\d]{10,15}$";

            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
        }

        public static bool TryParse(string value, out string phoneNumberValue)
        {
            try
            {
                PhoneNumber phoneNumber = new(value);
                phoneNumberValue = phoneNumber.Value;
                return true;
            }
            catch (System.Exception)
            {
                phoneNumberValue = "";
                return false;
            }
        }

        public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
        public static implicit operator PhoneNumber(string value) => new(value);
    }
}