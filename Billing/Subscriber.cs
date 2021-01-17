namespace Billing
{
    public class Subscriber
    {
        public string FirstName { get; }
        public string LastName { get; }
        public double Money { get; private set; }

        public Subscriber(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Money = 30;
        }

        public void AddMoney(double money)
        {
            Money += money;
        }

        public void RemoveMoney(double money)
        {
            Money -= money;
        }
    }
}
