namespace Banks
{
    public interface IAccountHandler
    {
        public IAccountHandler SetNext(IAccountHandler accountHandler);
        public bool Withdrawal(float money, IAccount account);
        public bool Replenishment(float money, IAccount account);
        public bool Transaction(float money, IAccount accountFrom, IAccount accountTo);
    }
}