using Mcm.Interactions.Domain.Enums;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Domain.ValueObjects
{
    public class Reminder : ValueObject
    {
        public TimeEnum Type { get; private set; }
        public double Value { get; private set; }
        public int Repeat { get; private set; }
        public int ReminderCount { get; private set; } = 0;

        private Reminder(){}
        private Reminder(string? type, double? value, int? repeat)
        {
            type ??= "MIN";
            if (Enum.TryParse<TimeEnum>(type, true, out TimeEnum parsedType))
                Type = parsedType;
            value ??= 10;
            if (value < 0)
                throw new DomainException("ReminderValue must not be lower 0");
            Value = Convert.ToDouble(value);
            Repeat = repeat ?? 1;
        }

        public static Reminder Create(string? type, double? value, int? repeat)
            => new(type, value, repeat);

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return Type.ToString();
            yield return Value.ToString();
            yield return Repeat.ToString();
        }

        public void MarkAsSent()
        {
            if (Repeat > ReminderCount)
                ReminderCount++;
        }

        public List<DateTime> GetReminderTimes(DateTime startDate)
        {
            List<DateTime> times = [];
            var interval = Type switch
            {
                TimeEnum.MINUTE => TimeSpan.FromMinutes(Value),
                TimeEnum.HEURE => TimeSpan.FromHours(Value),
                TimeEnum.JOUR => TimeSpan.FromDays(Value),
                _ => TimeSpan.FromMinutes(Value),
            };
            for (int i = 1; i <= Repeat; i++)
            {
                times.Add(startDate.Subtract(interval * i));
            }
            return times.OrderBy(t => t).ToList();
        }
    }
}