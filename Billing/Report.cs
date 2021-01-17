using System.Collections.Generic;

namespace Billing
{
    public class Report
    {
        public IList<ReportRecord> ListRecords;

        public Report()
        {
            ListRecords = new List<ReportRecord>();
        }

        public void AddRecord(ReportRecord record)
        {
            ListRecords.Add(record);
        }
        public IList<ReportRecord> GetRecords()
        {
            return ListRecords;
        }
    }
}
