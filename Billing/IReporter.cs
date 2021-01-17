using Task3.Billing.Enums;

namespace Billing
{
    public interface IReporter
    {
        void ShowRecords(Report report);
        Report SortCalls(Report report, SortType sortType);
    }
}
