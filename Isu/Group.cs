using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        private const int MAXSTUDENTCOUNT = 25;
        private CourseNumber _courseNumber;
        private string _groupName;
        private List<Student> _students;

        public Group(string groupName)
            : this(groupName, new List<Student>()) { }

        public Group(string groupName, List<Student> students)
        {
            if (groupName[0] != 'M' || groupName.Length != 5 || groupName[1] != '3')
            {
                throw new IsuException("Invalid group name:" + groupName);
            }

            int res;
            if (!int.TryParse(groupName.Substring(1), out res))
            {
                throw new IsuException("Invalid group name:" + groupName);
            }

            int courseNumber = int.Parse(groupName[2].ToString());
            if (courseNumber > 4 || courseNumber == 0)
            {
                throw new IsuException("Invalid group name:" + groupName);
            }

            _courseNumber = new CourseNumber(courseNumber);
            _groupName = groupName;
            _students = students;
        }

        public string GroupName => _groupName;
        public CourseNumber CourseNumber => _courseNumber;
        public List<Student> Students => new List<Student>(_students);

        public void AddStudent(Student student)
        {
            if (_students.Count == MAXSTUDENTCOUNT)
            {
                throw new IsuException("The maximum number of students in the group has been reached:" + _groupName);
            }

            _students.Add(student);
        }

        public Student FindStudentByName(string name)
        {
            foreach (var student in _students)
            {
                if (student.Name.Equals(name)) return student;
            }

            return null;
        }

        public Student FindStudentById(int id)
        {
            foreach (var student in _students)
            {
                if (student.Id == id) return student;
            }

            return null;
        }

        public void RemoveStudent(Student student)
        {
            _students.Remove(student);
        }
    }
}