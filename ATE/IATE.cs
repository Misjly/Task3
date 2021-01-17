using AutomaticTelephoneExchange.Args;
using Billing;
using System.Collections.Generic;

namespace AutomaticTelephoneExchange
{
    public interface IATE
    {
        Terminal GetNewTerminal(IContract contract);
        IContract RegisterContract(Subscriber subscriber, string phoneNumber, Tariff tariff);
        void CallingTo(object sender, ICallEventArgs e);
        IList<CallInfo> GetInfoList();
    }
}
