using System;

namespace Billing
{
    public class Reporter : IReporter
    {
        public Reporter()
        {

        }
        public void ShowRecords(Report report)
        {
            foreach (var record in report.GetRecords())
            {
                Console.WriteLine($"Calls:{Environment.NewLine}" +
                    $"Type {record.CallType}{Environment.NewLine}" +
                    $"Date: {record.Date}{Environment.NewLine}" +
                    $"Duration: {record.Time.ToString("mm: ss")}{Environment.NewLine}" +
                    $"Cost: {record.Cost} {Environment.NewLine}{Environment.NewLine}" +
                    $"Telephone number: {record.Number}{Environment.NewLine}");
            }
        }
    }
}
