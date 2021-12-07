namespace Banks
{
    public class BankBuilder : IBuilder
    {
        private Bank _bank;

        public BankBuilder()
        {
            _bank = new Bank();
        }

        public Bank GetResult()
        {
            return _bank;
        }

        public void SetName(string name)
        {
            _bank.Name = name;
        }

        public void SetCommission(float commission)
        {
            _bank.Commission = commission;
        }

        public void SetPayPercent(float payPercent)
        {
            _bank.PayPercent = payPercent;
        }

        public void SetCreditLimit(float creditLimit)
        {
            _bank.CreditLimit = creditLimit;
        }

        public void SetNotConfirmedLimit(float notConfirmedLimit)
        {
            _bank.NotConfirmedLimit = notConfirmedLimit;
        }
    }
}