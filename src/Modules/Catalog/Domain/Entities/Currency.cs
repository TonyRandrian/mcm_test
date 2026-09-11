using Mcm.Shared.Domain.Primitives;

namespace Mcm.Catalog.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Symbol { get; private set; } = string.Empty;

        private Currency() { }
        private Currency(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public static Currency Create(string name, string symbol)
            => new(name, symbol);

    }
}