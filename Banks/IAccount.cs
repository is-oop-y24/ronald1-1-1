namespace Banks
{
    public interface IAccount
    {
        public int Id { get; }
        public bool Replenishment(float money);
        public bool Withdrawal(float money);
        public bool Transaction(IAccount account, float money);

        public void DayPassed();
        public void ChangeMoney(float money);
    }
}