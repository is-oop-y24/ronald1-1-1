using System.Text.RegularExpressions;

namespace IsuExtra
{
    public class Class
    {
        private ClassTime _classTime;
        private string _teacher;

        public Class(ClassTime classTime,  string teacher)
        {
            _classTime = classTime;
            _teacher = teacher;
        }

        public ClassTime ClassTime => _classTime;
    }
}