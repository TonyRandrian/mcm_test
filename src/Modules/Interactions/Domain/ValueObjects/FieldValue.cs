using Mcm.Shared.Domain.Primitives;

namespace Mcm.Interactions.Domain.ValueObjects
{
    public class FieldValue : ValueObject
    {
        public Guid TypeFieldId { get; private set; }
        public string Value { get; private set; } = string.Empty;

        private FieldValue(){}
        private FieldValue(Guid typeFieldId, string value)
        {
            TypeFieldId = typeFieldId;
            Value = value;
        }

        public static FieldValue Create(Guid typeFieldId, string value)
            => new(typeFieldId, value);

        public void Update(string value)
        {
            Value = value;
        }

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return TypeFieldId.ToString();
            yield return Value;
        }
    }
}