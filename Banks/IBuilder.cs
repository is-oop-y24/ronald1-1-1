namespace Banks
{
    public interface IBuilder
    {
        public Bank GetResult();
        public void SetName(string name);
        public void SetCommission(float commission);
        public void SetPayPercent(float payPercent);
        public void SetCreditLimit(float creditLimit);
        public void SetNotConfirmedLimit(float notConfirmedLimit);
    }
}