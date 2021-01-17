using Billing.Enums;
using System;

namespace Billing
{
    public class ReportRecord
    {
        public CallType CallType { get; }
        public PhoneNumber Number { get; }
        public DateTime Date { get; }
        public DateTime Time { get; }
        public double Cost { get; }

        public ReportRecord(CallType callType, PhoneNumber number, DateTime date, DateTime time, double cost)
        {
            CallType = callType;
            Number = number;
            Date = date;
            Time = time;
            Cost = cost;
        }
    }
}
