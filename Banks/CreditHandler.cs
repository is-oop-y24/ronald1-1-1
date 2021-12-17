﻿using System;

namespace Banks
{
    public class CreditHandler : IAccountHandler
    {
        private IAccountHandler _next;
        public IAccountHandler SetNext(IAccountHandler accountHandler)
        {
            _next = accountHandler;
            return _next;
        }

        public bool Withdrawal(float money, IAccount account)
        {
            if (account is CreditAccount)
            {
                return account.Withdrawal(money);
            }

            return _next != null && _next.Withdrawal(money, account);
        }

        public bool Replenishment(float money, IAccount account)
        {
            if (account is CreditAccount)
            {
                return account.Replenishment(money);
            }

            return _next != null && _next.Replenishment(money, account);
        }

        public bool Transaction(float money, IAccount accountFrom, IAccount accountTo)
        {
            if (accountFrom is CreditAccount)
            {
                return accountFrom.Transaction(accountTo, money);
            }

            return _next != null && _next.Transaction(money, accountFrom, accountTo);
        }
    }
}