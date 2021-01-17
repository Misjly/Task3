namespace Billing
{
    public interface IBillingSystem
    {
        Report GetReport(PhoneNumber phoneNumber);
    }
}
