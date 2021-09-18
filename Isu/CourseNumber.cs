using System.Globalization;

namespace Isu
{
    public class CourseNumber
    {
        private int _number;

        public CourseNumber(int number)
        {
            _number = number;
        }

        public int Number
        {
            get => _number;
        }
    }
}