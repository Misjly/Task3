using AutomaticTelephoneExchange.Enums;
using Billing;
using System;

namespace AutomaticTelephoneExchange.Args
{
    public class AnswerEventArgs : EventArgs, ICallEventArgs
    {
        public PhoneNumber TelephoneNumber { get; }
        public PhoneNumber TargetTelephoneNumber { get; }
        public CallState StateInCall { get; }
        public Guid Id { get; }
        public AnswerEventArgs(PhoneNumber number, PhoneNumber target, CallState state, Guid id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            StateInCall = state;
            Id = id;
        }
    }
}
