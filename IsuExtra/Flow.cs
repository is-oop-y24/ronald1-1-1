using System.Collections.Generic;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Flow
    {
        private List<Class> _classes;
        private List<Student> _students;
        private string _name;
        private Course _course;

        public Flow(Course course, string name)
        {
            _name = name;
            _classes = new List<Class>();
            _students = new List<Student>();
            _course = course;
        }

        public Course Course => _course;
        public List<Class> Classes => _classes;
        public List<Student> Students => _students;

        public void AddStudent(Student student)
        {
            if (student.Group.GetFaculty() == _course.Faculty)
            {
                throw new IsuException("Student " + student.Name + " is in this faculty");
            }

            _students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            if (_students.Contains(student))
            {
                _students.Remove(student);
            }
            else
            {
                throw new IsuException("Student " + student.Name + "is not enrolled in this flow");
            }
        }

        public void AddClass(Class nclass)
        {
            _classes.Add(nclass);
        }
    }
}