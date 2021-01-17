using Billing;
using System;

namespace AutomaticTelephoneExchange.Args
{
    public interface ICallEventArgs
    {
        PhoneNumber TelephoneNumber { get; }
        PhoneNumber TargetTelephoneNumber { get; }
        Guid Id { get; }
    }
}
