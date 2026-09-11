using System.ComponentModel.DataAnnotations;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Shared.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; private set; } = string.Empty;

        private Name(){}
        public Name(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name argument invalid", nameof(value));
            Value = value.ToTitleCase();
        }

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator string(Name name) =>  name.Value;
        public static implicit operator Name(string value) =>  new(value);

    }
}