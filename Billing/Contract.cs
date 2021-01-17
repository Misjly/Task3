namespace Billing
{
    public class Contract : IContract
    {

        public Subscriber Subscriber { get; private set; }
        public PhoneNumber Number { get; private set; }
        public Tariff Tariff { get; }

        public Contract(Subscriber subscriber, string number, Tariff tariff)
        {
            Subscriber = subscriber;
            Number = new PhoneNumber(number);
            Tariff = tariff;
        }
    }
}
