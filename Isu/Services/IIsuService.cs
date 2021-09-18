using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group AddGroup(string name);
        Student AddStudent(Group group, string name);

        Student GetStudent(int id);
        Student FindStudent(string name);
        List<Student> FindStudents(string groupName);
        List<Student> FindStudents(CourseNumber courseNumber);

        Group FindGroup(string groupName);
        List<Group> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }

    public class IsuService : IIsuService
    {
        private readonly List<Group> _groups;
        private int _studentId;

        public IsuService()
        {
            _studentId = 0;
            _groups = new List<Group>();
        }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(group, name, ++_studentId);
            group.AddStudent(student);
            return student;
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in _groups)
            {
                Student student = group.FindStudentByName(name);
                if (student != null) return student;
            }

            throw new IsuException("That student not found:" + name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return FindGroup(groupName).Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Group group in _groups)
            {
                if (group.CourseNumber.Equals(courseNumber))
                {
                    students.AddRange(group.Students);
                }
            }

            return students;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in _groups)
            {
                Student student = group.FindStudentById(id);
                if (student != null) return student;
            }

            throw new IsuException("Student with that id not found:" + id);
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.GroupName.Equals(groupName)) return group;
            }

            throw new IsuException("That group not found:" + groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var groups = new List<Group>();
            foreach (Group group in _groups)
            {
                if (group.CourseNumber.Number == courseNumber.Number) groups.Add(group);
            }

            return groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.Group.RemoveStudent(student);
            newGroup.AddStudent(student);
            student.Group = newGroup;
        }
    }
}