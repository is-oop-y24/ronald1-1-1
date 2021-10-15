namespace IsuExtra.Utils
{
    public class Time
    {
        private int _hours;
        private int _minutes;

        public Time(int hours, int minutes)
        {
            _hours = hours;
            _minutes = minutes;
        }

        public int Hours => _hours;
        public int Minutes => _minutes;

        public override string ToString()
        {
            return _hours + ":" + _minutes;
        }

        public int ToMinutes()
        {
            return (_hours * 60) + _minutes;
        }
    }
}