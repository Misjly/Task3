namespace Billing
{
    public class Tariff
    {
        public int CostOfMonth { get; }
        public int CostOfCallPerMinute { get; }
        public Tariff(int costOfMonth, int costOfCallPerMinute)
        {
            CostOfMonth = costOfMonth;
            CostOfCallPerMinute = costOfCallPerMinute;
        }
    }
}
