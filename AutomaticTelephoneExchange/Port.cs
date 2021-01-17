using AutomaticTelephoneExchange.Args;
using AutomaticTelephoneExchange.Enums;
using Billing;
using System;

namespace AutomaticTelephoneExchange
{
    public class Port
    {
        public PortState State;
        public bool Flag;

        public event EventHandler<CallEventArgs> CallPortEvent;
        public event EventHandler<AnswerEventArgs> AnswerPortEvent;
        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<AnswerEventArgs> AnswerEvent;

        public event EventHandler<EndCallEventArgs> EndCallEvent;

        public Port()
        {
            State = PortState.Disconnect;
            Flag = false;
        }

        public bool Connect(Terminal terminal)
        {
            if (State == PortState.Disconnect)
            {
                State = PortState.Connect;
                terminal.CallEvent += CallingTo;
                terminal.AnswerEvent += AnswerTo;
                terminal.EndCallEvent += EndCall;
                Flag = true;
            }
            return Flag;
        }

        public bool Disconnect(Terminal terminal)
        {
            if (State != PortState.Disconnect)
            {
                State = PortState.Disconnect;
                terminal.CallEvent -= CallingTo;
                terminal.AnswerEvent -= AnswerTo;
                terminal.EndCallEvent -= EndCall;
                Flag = false;
            }
            return false;
        }

        protected virtual void RaiseIncomingCallEvent(PhoneNumber number, PhoneNumber targetNumber, Guid id)
        {
            CallPortEvent?.Invoke(this, new CallEventArgs(number, targetNumber, id));
        }

        protected virtual void RaiseAnswerCallEvent(PhoneNumber number, PhoneNumber targetNumber, CallState state, Guid id)
        {
            AnswerPortEvent?.Invoke(this, new AnswerEventArgs(number, targetNumber, state, id));
            State = PortState.InCall;
        }

        protected virtual void RaiseCallingToEvent(PhoneNumber number, PhoneNumber targetNumber, Guid id)
        {
            CallEvent?.Invoke(this, new CallEventArgs(number, targetNumber, id));
        }

        protected virtual void RaiseAnswerToEvent(AnswerEventArgs eventArgs)
        {
            AnswerEvent?.Invoke(this, new AnswerEventArgs(
                eventArgs.TelephoneNumber,
                eventArgs.TargetTelephoneNumber,
                eventArgs.StateInCall,
                eventArgs.Id));
        }

        protected virtual void RaiseEndCallEvent(PhoneNumber number, PhoneNumber targetNumber, Guid id)
        {
            EndCallEvent?.Invoke(this, new EndCallEventArgs(number, targetNumber, id));
            State = PortState.Connect;
        }


        private void CallingTo(object sender, CallEventArgs e)
        {
            RaiseCallingToEvent(e.TelephoneNumber, e.TargetTelephoneNumber, e.Id);
        }

        private void AnswerTo(object sender, AnswerEventArgs e)
        {
            RaiseAnswerToEvent(e);
        }


        private void EndCall(object sender, EndCallEventArgs e)
        {
            RaiseEndCallEvent(e.TelephoneNumber, e.TargetTelephoneNumber, e.Id);
        }

        public void IncomingCall(PhoneNumber number, PhoneNumber targetNumber, Guid id)
        {
            RaiseIncomingCallEvent(number, targetNumber, id);
        }

        public void AnswerCall(PhoneNumber number, PhoneNumber targetNumber, CallState state, Guid id)
        {
            RaiseAnswerCallEvent(number, targetNumber, state, id);
        }
    }
}
