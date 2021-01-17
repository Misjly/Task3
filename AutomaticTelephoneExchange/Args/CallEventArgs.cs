using Billing;
using System;

namespace AutomaticTelephoneExchange.Args
{
    public class CallEventArgs : EventArgs, ICallEventArgs
    {
        public PhoneNumber TelephoneNumber { get; }
        public PhoneNumber TargetTelephoneNumber { get; }
        public Guid Id { get; }


        public CallEventArgs(PhoneNumber number, PhoneNumber target, Guid id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            Id = id;
        }
    }
}
