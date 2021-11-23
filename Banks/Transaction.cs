namespace Banks
{
    public class Transaction
    {
        private static int id = 0;
        private IAccount _accountFrom;
        private IAccount _accountTo;
        private float _money;
        private int _id;

        public Transaction(IAccount accountFrom, IAccount accountTo, float money)
        {
            _accountFrom = accountFrom;
            _accountTo = accountTo;
            _money = money;
            _id = ++id;
        }

        public IAccount AccountFrom => _accountFrom;
        public IAccount AccountTo => _accountTo;
        public float Money => _money;
        public int Id => _id;

        public void Cancel()
        {
            AccountFrom.ChangeMoney(_money);
            AccountTo.ChangeMoney(-_money);
        }
    }
}