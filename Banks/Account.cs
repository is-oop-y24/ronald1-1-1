using System;
using System.Data.Common;

namespace Banks
{
    public class Account : IAccount
    {
        private static int id = 0;

        public Account(int clientId, CentralBank centralBank, Bank bank)
        {
            ClientId = clientId;
            CentralBank = centralBank;
            Bank = bank;
            Money = 0f;
            Id = ++id;
        }

        public float Money { get; protected set; }

        public int Id { get; }
        protected int ClientId { get; set; }
        protected Bank Bank { get; }
        protected CentralBank CentralBank { get; }

        public virtual bool Replenishment(float money)
        {
            Money += money;
            return true;
        }

        public virtual bool Withdrawal(float money)
        {
            if (!CentralBank.FindClient(ClientId).IsConfirmed())
            {
                if (money > Bank.NotConfirmedLimit)
                {
                    return false;
                }
            }

            if (Money < money)
            {
                return false;
            }

            Money -= money;
            return true;
        }

        public virtual bool Transaction(IAccount account, float money)
        {
            if (Withdrawal(money))
            {
                account.Replenishment(money);
                CentralBank.AddTransaction(new Transaction(this, account, money));
                return true;
            }

            return false;
        }

        public void ChangeMoney(float money)
        {
            Money += money;
        }

        public virtual void DayPassed()
        {
        }
    }
}