using System.Globalization;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Domain.ValueObjects
{
    public class DataTime : ValueObject
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        private DataTime(){}
        private DataTime(string startDate, string endDate)
        {
            StartDate = DateTimeOffset.Parse(startDate).UtcDateTime;
            EndDate = DateTimeOffset.Parse(endDate).UtcDateTime;
            if (StartDate > EndDate)
            {
                throw new DomainException("EndDate Must be greater than StartDate");
            }
        }

        public static DataTime Create(string startDate, string endDate)
            => new(startDate, endDate);

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return StartDate.ToString();
            yield return EndDate.ToString();
        }
        
        public bool InSheduledTime(DateTime startTime, DateTime endTime)
        {
            return (this.StartDate < startTime && this.EndDate > startTime)
                || (this.StartDate < endTime && this.EndDate > endTime);
        }

    }
}