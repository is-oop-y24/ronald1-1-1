namespace Banks
{
    public class DepositHandler : IAccountHandler
    {
        private IAccountHandler _next;
        public IAccountHandler SetNext(IAccountHandler accountHandler)
        {
            _next = accountHandler;
            return accountHandler;
        }

        public bool Withdrawal(float money, IAccount account)
        {
            if (account is DepositAccount)
            {
                return account.Withdrawal(money);
            }

            return _next != null && _next.Withdrawal(money, account);
        }

        public bool Replenishment(float money, IAccount account)
        {
            if (account is DepositAccount)
            {
                return account.Replenishment(money);
            }

            return _next != null && _next.Replenishment(money, account);
        }

        public bool Transaction(float money, IAccount accountFrom, IAccount accountTo)
        {
            if (accountFrom is DepositAccount)
            {
                return accountFrom.Transaction(accountTo, money);
            }

            return _next != null && _next.Transaction(money, accountFrom, accountTo);
        }
    }
}