using System;
using System.Linq;
using Task3.Billing.Enums;

namespace Billing
{
    public class Reporter : IReporter
    {
        public Reporter()
        {

        }
        public void ShowRecords(Report report)
        {
            foreach (var record in report.ListRecords)
            {
                Console.WriteLine($"Calls:{Environment.NewLine}" +
                    $"Type {record.CallType}{Environment.NewLine}" +
                    $"Date: {record.Date}{Environment.NewLine}" +
                    $"Duration: {record.Time.ToString("mm: ss")}{Environment.NewLine}" +
                    $"Cost: {record.Cost} {Environment.NewLine}{Environment.NewLine}" +
                    $"Telephone number: {record.Number}{Environment.NewLine}");
            }
        }

        public Report SortCalls(Report report, SortType sortType)
        {
            switch (sortType)
            {
                case SortType.SortByDate:
                    {
                        report.ListRecords.OrderBy(x => x.Date).ToList();
                        return report;
                    }

                case SortType.SortByCost:
                    {
                        report.ListRecords.OrderBy(x => x.Cost).ToList();
                        return report;
                    }

                case SortType.SortByNumber:
                    {
                        report.ListRecords.OrderBy(x => x.Number).ToList();
                        return report;
                    }

                default:
                    return report;
            }
        }
    }
}
