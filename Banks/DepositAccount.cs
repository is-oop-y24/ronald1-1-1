using System;

namespace Banks
{
    public class DepositAccount : Account
    {
        private int _timeLimit;
        private int _time;
        private float _moneyBuffer;
        public DepositAccount(int clientId,  CentralBank centralBank, Bank bank, int timeLimit)
            : base(clientId, centralBank, bank)
        {
            _timeLimit = timeLimit;
            _time = 0;
            _moneyBuffer = 0;
        }

        public override bool Transaction(IAccount account, float money)
        {
            if (_time < _timeLimit)
            {
                return false;
            }

            return base.Transaction(account, money);
        }

        public override bool Withdrawal(float money)
        {
            if (_time < _timeLimit)
            {
                return false;
            }

            return base.Withdrawal(money);
        }

        public override void DayPassed()
        {
            _time++;
            if (_time <= _timeLimit)
            {
                _moneyBuffer += (Money * Bank.PayPercent) / (365 * 100);
            }

            if (_time == _timeLimit)
            {
                Money += _moneyBuffer;
                _moneyBuffer = 0;
            }
        }
    }
}