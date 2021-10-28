using System.Threading;
using IsuExtra.Utils;

namespace IsuExtra
{
    public class ClassTime
    {
        private Time _beginTime;
        private Time _endTime;

        public ClassTime(Time beginTime, Time endTime)
        {
            _beginTime = beginTime;
            _endTime = endTime;
        }

        public Time BeginTime => _beginTime;
        public Time EndTime => _endTime;

        public bool Collision(ClassTime classTime)
        {
            int minutesBegin1 = _beginTime.ToMinutes();
            int minutesEnd1 = _endTime.ToMinutes();
            int minutesBegin2 = classTime.BeginTime.ToMinutes();
            int minutesEnd2 = classTime.EndTime.ToMinutes();
            if ((minutesBegin2 >= minutesBegin1 && minutesBegin2 <= minutesEnd1) || (minutesBegin1 >= minutesBegin2 && minutesBegin1 <= minutesEnd2))
            {
                return true;
            }

            return false;
        }
    }
}