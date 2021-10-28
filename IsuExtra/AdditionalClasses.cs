using System.Collections.Generic;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class AdditionalClasses
    {
        private const int MAXCOURSESFORSTUDENT = 2;
        private List<Course> _courses;
        private Dictionary<Student, List<Flow>> _studentHasFlows;
        private Dictionary<Group, List<Class>> _groupShedule;

        public AdditionalClasses()
        {
            _courses = new List<Course>();
            _studentHasFlows = new Dictionary<Student, List<Flow>>();
            _groupShedule = new Dictionary<Group, List<Class>>();
        }

        public Course AddCourse(string name, char faculty)
        {
            var course = new Course(name, faculty);
            _courses.Add(course);
            return course;
        }

        public Flow AddFlowToCourse(Course course, string name)
        {
            return course.AddFlow(name);
        }

        public void AddStudentToFlow(Flow flow, Student student)
        {
            if (_groupShedule.ContainsKey(student.Group))
            {
                foreach (var groupClass in _groupShedule[student.Group])
                {
                    foreach (var flowClass in flow.Classes)
                    {
                        if (groupClass.ClassTime.Collision(flowClass.ClassTime))
                        {
                            throw new IsuException("Schedules overlap");
                        }
                    }
                }
            }

            if (_studentHasFlows.ContainsKey(student))
            {
                List<Flow> flows = _studentHasFlows[student];
                foreach (var studentFlow in _studentHasFlows[student])
                {
                    if (flow.Course.Equals(studentFlow.Course))
                    {
                        throw new IsuException("Student " + student.Name + "is already enrolled in this courses");
                    }
                }

                if (flows.Count == MAXCOURSESFORSTUDENT)
                {
                    throw new IsuException("Student " + student.Name + " already has " + MAXCOURSESFORSTUDENT + " courses");
                }

                flow.AddStudent(student);
                flows.Add(flow);
            }
            else
            {
                flow.AddStudent(student);
                _studentHasFlows.Add(student, new List<Flow>() { flow });
            }
        }

        public void RemoveStudentFromFlow(Flow flow, Student student)
        {
            flow.RemoveStudent(student);
            _studentHasFlows[student].Remove(flow);
        }

        public void AddClassToFlow(Flow flow, Class nclass)
        {
            flow.AddClass(nclass);
        }

        public void AddClassToGroup(Group group, Class nclass)
        {
            if (_groupShedule.ContainsKey(group))
            {
                _groupShedule[group].Add(nclass);
            }
            else
            {
                _groupShedule.Add(group, new List<Class>() { nclass });
            }
        }

        public void RemoveClassFromGroup(Group group, Class nclass)
        {
            _groupShedule[group].Remove(nclass);
        }

        public List<Flow> GetFlowsFromCourse(Course course)
        {
            return course.Flows;
        }

        public List<Student> GetStudentsFromFlow(Flow flow)
        {
            return flow.Students;
        }

        public List<Student> GetNotAddedStudentsFromGroup(Group group)
        {
            var notAddedStudents = new List<Student>();
            foreach (var student in group.Students)
            {
                if (_studentHasFlows.ContainsKey(student))
                {
                    if (_studentHasFlows[student].Count < 2)
                    {
                        notAddedStudents.Add(student);
                    }
                }
                else
                {
                    notAddedStudents.Add(student);
                }
            }

            return notAddedStudents;
        }

        public List<Class> GetClassesFromFlow(Flow flow)
        {
            return flow.Classes;
        }
    }
}