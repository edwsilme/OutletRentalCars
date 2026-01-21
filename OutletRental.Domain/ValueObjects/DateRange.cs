using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Domain.ValueObjects
{
    public class DateRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateRange(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                throw new ArgumentException("Start date must be before end date");
            }

            Start = start;
            End = end;
        }

        public bool Overlaps(DateRange other)
        {
            return Start < other.End && End > other.Start;
        }
    }
}
