using Billing;
using System;

namespace AutomaticTelephoneExchange.Args
{
    public class EndCallEventArgs : EventArgs, ICallEventArgs
    {
        public PhoneNumber TelephoneNumber { get; }
        public PhoneNumber TargetTelephoneNumber { get; }
        public Guid Id { get; }


        public EndCallEventArgs(PhoneNumber number, PhoneNumber target, Guid id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            Id = id;
        }
    }
}
