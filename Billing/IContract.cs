namespace Billing
{
    public interface IContract
    {
        Subscriber Subscriber { get; }
        Tariff Tariff { get; }
        PhoneNumber Number { get; }
    }
}
