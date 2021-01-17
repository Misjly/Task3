using AutomaticTelephoneExchange;
using AutomaticTelephoneExchange.Enums;
using Billing;
using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {

            int costOfMonth = 20;
            int costPerMinute = 2;


            IATE automaticTelephoneExchange = new ATE();

            Tariff tariff = new Tariff(costOfMonth, costPerMinute);

            IContract contract1 = automaticTelephoneExchange.RegisterContract(new Subscriber("Ivan", "Ivanov"), "1234567", tariff);
            IContract contract2 = automaticTelephoneExchange.RegisterContract(new Subscriber("Vasia", "Vasilyev"), "7654321", tariff);
            IContract contract3 = automaticTelephoneExchange.RegisterContract(new Subscriber("Peter", "Petrov"), "4321765", tariff);

            var terminal1 = automaticTelephoneExchange.GetNewTerminal(contract1);
            var terminal2 = automaticTelephoneExchange.GetNewTerminal(contract2);
            var terminal3 = automaticTelephoneExchange.GetNewTerminal(contract3);

            terminal1.ConnectToPort();
            terminal2.ConnectToPort();
            terminal3.ConnectToPort();

            terminal1.Call(terminal2.Number);
            if (terminal1.TerminalPort.State == PortState.InCall)
                terminal1.EndCall(terminal2.Number);

            Console.WriteLine();
            Console.WriteLine();


            terminal3.Call(terminal1.Number);
            if (terminal3.TerminalPort.State == PortState.InCall)
                terminal3.EndCall(terminal1.Number);

            Console.WriteLine();
            Console.WriteLine();

            terminal2.Call(terminal1.Number);
            if (terminal2.TerminalPort.State == PortState.InCall)
                terminal2.EndCall(terminal1.Number);

                IBillingSystem billingSystem = new BillingSystem(automaticTelephoneExchange.GetInfoList());
            IReporter reporter = new Reporter();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            reporter.ShowRecords(billingSystem.GetReport(terminal1.Number));
            reporter.ShowRecords(billingSystem.GetReport(terminal2.Number));
            reporter.ShowRecords(billingSystem.GetReport(terminal3.Number));

        }
    }
}
