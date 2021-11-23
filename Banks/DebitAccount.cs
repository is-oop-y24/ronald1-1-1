using System;

namespace Banks
{
    public class DebitAccount : Account
    {
        private float _moneyBuffer;
        private int _monthDays;

        public DebitAccount(int clientId, CentralBank centralBank, Bank bank)
            : base(clientId, centralBank, bank)
        {
            _moneyBuffer = 0;
            _monthDays = 0;
        }

        public override void DayPassed()
        {
            _moneyBuffer += (Money * Bank.PayPercent) / (365 * 100);
            _monthDays++;
            if (_monthDays == 30)
            {
                _monthDays = 0;
                MonthPassed();
            }
        }

        private void MonthPassed()
        {
            Money = Money + _moneyBuffer;
        }
    }
}