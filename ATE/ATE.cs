using AutomaticTelephoneExchange.Args;
using AutomaticTelephoneExchange.Enums;
using Billing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticTelephoneExchange
{
    public class ATE : IATE
    {
        private IDictionary<PhoneNumber, Tuple<Port, IContract>> _usersData;
        private IList<CallInfo> _callList { get; }

        public ATE()
        {
            _usersData = new Dictionary<PhoneNumber, Tuple<Port, IContract>>();
            _callList = new List<CallInfo>();
        }

        public Terminal GetNewTerminal(IContract contract)
        {
            var newPort = new Port();
            newPort.AnswerEvent += CallingTo;
            newPort.CallEvent += CallingTo;
            newPort.EndCallEvent += CallingTo;
            _usersData.Add(contract.Number, new Tuple<Port, IContract>(newPort, contract));
            var newTerminal = new Terminal(contract.Number, newPort);
            return newTerminal;
        }

        public IContract RegisterContract(Subscriber subscriber, string number, Tariff tariff)
        {
            var contract = new Contract(subscriber, number, tariff);
            return contract;
        }

        public void CallingTo(object sender, ICallEventArgs e)
        {
            if (!((_usersData.ContainsKey(e.TargetTelephoneNumber) && e.TargetTelephoneNumber != e.TelephoneNumber)
                || e is EndCallEventArgs))
            {
                if (!_usersData.ContainsKey(e.TargetTelephoneNumber))
                {
                    Console.WriteLine("You are calling to a non-existent number");
                }
                else
                {
                    Console.WriteLine("You are calling to your number");
                }
                return;
            }

            CallInfo info = null;
            Port targetPort;
            Port port;
            PhoneNumber number;
            PhoneNumber targetNumber;
            if (e is EndCallEventArgs)
            {

                var callListFirst = _callList.First(x => x.Id.Equals(e.Id));
                if (callListFirst.MyNumber == e.TelephoneNumber)
                {
                    targetPort = _usersData[callListFirst.TargetNumber].Item1;
                    port = _usersData[callListFirst.MyNumber].Item1;
                    number = callListFirst.MyNumber;
                    targetNumber = callListFirst.TargetNumber;
                }
                else
                {
                    port = _usersData[callListFirst.TargetNumber].Item1;
                    targetPort = _usersData[callListFirst.MyNumber].Item1;
                    targetNumber = callListFirst.MyNumber;
                    number = callListFirst.TargetNumber;
                }
            }
            else
            {
                targetPort = _usersData[e.TargetTelephoneNumber].Item1;
                port = _usersData[e.TelephoneNumber].Item1;
                targetNumber = e.TargetTelephoneNumber;
                number = e.TelephoneNumber;
            }
            if (targetPort.State != PortState.Disconnect && port.State != PortState.Disconnect)
            {
                var tuple = _usersData[number];
                var targetTuple = _usersData[targetNumber];

                if (e is AnswerEventArgs)
                {
                    var answerArgs = e as AnswerEventArgs;

                    if (!answerArgs.Id.Equals(Guid.Empty) && _callList.Any(x => x.Id.Equals(answerArgs.Id)))
                    {
                        info = _callList.First(x => x.Id.Equals(answerArgs.Id));
                    }

                    targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall, info.Id);
                }
                if (e is CallEventArgs)
                {
                    if (tuple.Item2.Subscriber.Money > tuple.Item2.Tariff.CostOfCallPerMinute)
                    {
                        var callArgs = e as CallEventArgs;

                        if (callArgs.Id.Equals(Guid.Empty) || !_callList.Any(x => x.Id.Equals(callArgs.Id)))
                        {
                            info = new CallInfo(
                                callArgs.TelephoneNumber,
                                callArgs.TargetTelephoneNumber,
                                DateTime.Now);
                            _callList.Add(info);
                        }

                        if (!callArgs.Id.Equals(Guid.Empty) && _callList.Any(x => x.Id.Equals(callArgs.Id)))
                        {
                            info = _callList.First(x => x.Id.Equals(callArgs.Id));
                        }

                        targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber, info.Id);
                    }
                    else
                    {
                        Console.WriteLine($"Terminal with number {e.TelephoneNumber} doesn't have enough money on account!");

                    }
                }
                if (e is EndCallEventArgs)
                {
                    var args = e as EndCallEventArgs;
                    _callList.First(x => x.Id.Equals(args.Id)).EndCall = DateTime.Now;

                    info = _callList.First(x => x.Id.Equals(args.Id));

                    _callList.First(x => x.Id.Equals(args.Id)).Cost =
                        tuple.Item2.Tariff.CostOfCallPerMinute * TimeSpan.FromTicks((info.EndCall - info.BeginCall).Ticks).TotalMinutes;

                    targetTuple.Item2.Subscriber.RemoveMoney(info.Cost);
                    targetPort.AnswerCall(args.TelephoneNumber, args.TargetTelephoneNumber, CallState.Rejected, info.Id);

                    if (_callList.Any(x => x.Id.Equals(e.Id)))
                    {
                        _callList.First(x => x.Id.Equals(e.Id)).Id = Guid.NewGuid();
                    }
                }
            }
        }

        public IList<CallInfo> GetInfoList()
        {
            return _callList;
        }
    }
}
