using System;

namespace Banks
{
    public class CreditAccount : Account
    {
        public CreditAccount(int clientId, CentralBank centralBank, Bank bank)
            : base(clientId, centralBank, bank)
        {
        }

        public override bool Transaction(IAccount account, float money)
        {
            if (Money - money >= -Bank.CreditLimit)
            {
                return base.Transaction(account, money);
            }

            return false;
        }

        public override bool Withdrawal(float money)
        {
            if (Money - money >= -Bank.CreditLimit)
            {
                if (!CentralBank.FindClient(ClientId).IsConfirmed())
                {
                    if (money > Bank.NotConfirmedLimit)
                    {
                        return false;
                    }
                }

                Money -= money;
                return true;
            }

            return false;
        }

        public override void DayPassed()
        {
            if (Money < 0 && Money > -Bank.CreditLimit)
            {
                Money -= Bank.Commission;
            }
        }
    }
}