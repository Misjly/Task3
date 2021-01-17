using AutomaticTelephoneExchange.Args;
using AutomaticTelephoneExchange.Enums;
using Billing;
using System;

namespace AutomaticTelephoneExchange
{
    public class Terminal
    {
        public PhoneNumber Number { get; }
        public Port TerminalPort { get; }
        private Guid Id { get; set; }

        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<AnswerEventArgs> AnswerEvent;
        public event EventHandler<EndCallEventArgs> EndCallEvent;
        public Terminal(PhoneNumber number, Port port)
        {
            Number = number;
            TerminalPort = port;
        }
        protected virtual void RaiseCallEvent(PhoneNumber targetNumber, Guid id)
        {
            CallEvent?.Invoke(this, new CallEventArgs(Number, targetNumber, id));
        }

        protected virtual void RaiseAnswerEvent(PhoneNumber targetNumber, CallState state, Guid id)
        {
            AnswerEvent?.Invoke(this, new AnswerEventArgs(Number, targetNumber, state, id));
        }

        protected virtual void RaiseEndCallEvent(PhoneNumber targetNumber, Guid id)
        {
            EndCallEvent?.Invoke(this, new EndCallEventArgs(Number, targetNumber, id));
        }

        public void Call(PhoneNumber targetNumber)
        {
            RaiseCallEvent(targetNumber, Id);
        }

        public void TakeIncomingCall(object sender, CallEventArgs e)
        {
            bool answerFlag = false;
            Id = e.Id;
            Console.WriteLine($"Have incoming Call at number: {e.TelephoneNumber} to terminal {e.TargetTelephoneNumber}");
            do
            {
                Console.WriteLine("Answer? Y/N");
                char k = Console.ReadKey().KeyChar;
                if (k == 'Y' || k == 'y')
                {
                    answerFlag = true;
                    Console.WriteLine();
                    AnswerToCall(e.TelephoneNumber, CallState.Answered);
                }
                else if (k == 'N' || k == 'n')
                {
                    answerFlag = true;
                    Console.WriteLine();
                    EndCall(e.TargetTelephoneNumber);
                }
                else
                {
                    Console.WriteLine();
                }
            } while (answerFlag == false);
        }

        public void ConnectToPort()
        {
            if (TerminalPort.Connect(this))
            {
                TerminalPort.CallPortEvent += TakeIncomingCall;
                TerminalPort.AnswerPortEvent += TakeAnswer;
            }
        }

        public void AnswerToCall(PhoneNumber target, CallState state)
        {
            RaiseAnswerEvent(target, state, Id);
        }

        public void EndCall(PhoneNumber targetNumber)
        {
            RaiseEndCallEvent(targetNumber, Id);
        }

        public void TakeAnswer(object sender, AnswerEventArgs e)
        {
            Id = e.Id;
            if (e.StateInCall == CallState.Answered)
            {
                Console.WriteLine($"Terminal with number {e.TargetTelephoneNumber} have answered on call from number {e.TelephoneNumber}");
            }
            else
            {
                Console.WriteLine($"Terminal with number {e.TelephoneNumber} hung up");
            }
        }
    }
}
