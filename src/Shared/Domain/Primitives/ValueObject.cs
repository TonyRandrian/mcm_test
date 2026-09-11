namespace Mcm.Shared.Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<string> GetAtomicValues();

        public override bool Equals(object? obj)
        {
            return obj is ValueObject valueObject && ValuesEqual(valueObject);
        }
        public bool Equals(ValueObject? other) => this.Equals(other);

        public override int GetHashCode() => GetAtomicValues()
                .Aggregate(
                    default(int),
                    HashCode.Combine);
        private bool ValuesEqual(ValueObject obj) => GetAtomicValues().SequenceEqual(obj.GetAtomicValues());

    }
}