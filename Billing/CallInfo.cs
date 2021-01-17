using System;

namespace Billing
{
    public class CallInfo
    {
        public PhoneNumber MyNumber { get; set; }
        public PhoneNumber TargetNumber { get; set; }
        public Guid Id { get; set; }
        public DateTime BeginCall { get; set; }
        public DateTime EndCall { get; set; }
        public double Cost { get; set; }

        public CallInfo(PhoneNumber myNumber, PhoneNumber targetNumber, DateTime beginCall)
        {
            MyNumber = myNumber;
            TargetNumber = targetNumber;
            BeginCall = beginCall;
            Id = Guid.NewGuid();
        }
    }
}
