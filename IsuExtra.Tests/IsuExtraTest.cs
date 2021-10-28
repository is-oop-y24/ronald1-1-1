using Isu;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Utils;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private AdditionalClasses _additionalClasses;
        private IsuService _isuService;
        
        [SetUp]
        public void Setup()
        {
            _additionalClasses = new AdditionalClasses();
            _isuService = new IsuService();
        }

        [Test]
        public void AddNewAdditionalClass()
        {
            Course course = _additionalClasses.AddCourse("1", 'M');
            Flow flow = _additionalClasses.AddFlowToCourse(course, "1.1");
            var class1 = new Class(new ClassTime(new Time(8,20),new Time(9,50)), "Oleg");
            _additionalClasses.AddClassToFlow(flow, class1);
            Assert.True(_additionalClasses.GetClassesFromFlow(flow)[0].Equals(class1));
        }

        [Test]
        public void AddStudentToCourse()
        {
            Course course1 = _additionalClasses.AddCourse("1", 'M');
            Flow flow1 = _additionalClasses.AddFlowToCourse(course1, "1.1");
            Flow flow12 = _additionalClasses.AddFlowToCourse(course1, "1.2");
            Course course2 = _additionalClasses.AddCourse("2", 'Z');
            Flow flow2 = _additionalClasses.AddFlowToCourse(course2, "2.1");
            Course course3 = _additionalClasses.AddCourse("3", 'P');
            Flow flow3 = _additionalClasses.AddFlowToCourse(course3, "3.1");
            var class1 = new Class(new ClassTime(new Time(8,20),new Time(9,50)), "Oleg");
            Group group1 = _isuService.AddGroup("A3105");
            Student student1 = _isuService.AddStudent(group1, "Lesha");
            _additionalClasses.AddStudentToFlow(flow1, student1);
            Assert.Catch<IsuException>(() => _additionalClasses.AddStudentToFlow(flow12, student1));
            _additionalClasses.AddStudentToFlow(flow2, student1);
            Assert.True(_additionalClasses.GetStudentsFromFlow(flow1).Contains(student1));
            Assert.True(_additionalClasses.GetStudentsFromFlow(flow2).Contains(student1));
            Assert.Catch<IsuException>(() => _additionalClasses.AddStudentToFlow(flow3, student1));
            _additionalClasses.RemoveStudentFromFlow(flow1, student1);
            _additionalClasses.AddStudentToFlow(flow3, student1);
            Group group2 = _isuService.AddGroup("M3105");
            Student student2 = _isuService.AddStudent(group2, "Grisha");
            Assert.Catch<IsuException>(() => _additionalClasses.AddStudentToFlow(flow1, student2));
            var class2 = new Class(new ClassTime(new Time(8,20),new Time(9,50)), "Pasha");
            _additionalClasses.AddClassToGroup(group2, class1);
            _additionalClasses.AddClassToFlow(flow3, class2);
            Assert.Catch<IsuException>(() => _additionalClasses.AddStudentToFlow(flow3, student2));
        }

        [Test]
        public void GetFlowsFromCourse()
        {
            Course course1 = _additionalClasses.AddCourse("1", 'M');
            Course course2 = _additionalClasses.AddCourse("2", 'T');
            Flow flow1 = _additionalClasses.AddFlowToCourse(course1, "1.1");
            Flow flow2 = _additionalClasses.AddFlowToCourse(course1, "1.2");
            Flow flow3 = _additionalClasses.AddFlowToCourse(course2, "2.1");
            Assert.True(_additionalClasses.GetFlowsFromCourse(course1).Contains(flow1));
            Assert.True(_additionalClasses.GetFlowsFromCourse(course1).Contains(flow2));
            Assert.False(_additionalClasses.GetFlowsFromCourse(course1).Contains(flow3));
        }

        [Test]
        public void GetStudentFromFlow()
        {
            Course course1 = _additionalClasses.AddCourse("1", 'M');
            Flow flow1 = _additionalClasses.AddFlowToCourse(course1, "1.1");
            Group group1 = _isuService.AddGroup("A3105");
            Student student1 = _isuService.AddStudent(group1, "Lesha");
            Student student2 = _isuService.AddStudent(group1, "Gleb");
            _additionalClasses.AddStudentToFlow(flow1, student1);
            _additionalClasses.AddStudentToFlow(flow1, student2);
            Assert.True(_additionalClasses.GetStudentsFromFlow(flow1).Contains(student1) &&
                        _additionalClasses.GetStudentsFromFlow(flow1).Contains(student2));
            
        }

        [Test]
        public void GetNotAddedStudents()
        {
            Course course1 = _additionalClasses.AddCourse("1", 'M');
            Flow flow1 = _additionalClasses.AddFlowToCourse(course1, "1.1");
            Course course2 = _additionalClasses.AddCourse("2", 'Z');
            Flow flow2 = _additionalClasses.AddFlowToCourse(course2, "2.1");
            Course course3 = _additionalClasses.AddCourse("3", 'P');
            Flow flow3 = _additionalClasses.AddFlowToCourse(course3, "3.1");
            Group group1 = _isuService.AddGroup("A3105");
            Student student1 = _isuService.AddStudent(group1, "Lesha");
            Group group2 = _isuService.AddGroup("M3105");
            Student student2 = _isuService.AddStudent(group2, "Grisha");
            Group group3 = _isuService.AddGroup("M3105");
            Student student3 = _isuService.AddStudent(group3, "Monya");
            _additionalClasses.AddStudentToFlow(flow1, student1);
            _additionalClasses.AddStudentToFlow(flow2, student1);
            _additionalClasses.AddStudentToFlow(flow3, student2);
            Assert.False(_additionalClasses.GetNotAddedStudentsFromGroup(group1).Contains(student1));
            Assert.True(_additionalClasses.GetNotAddedStudentsFromGroup(group2).Contains(student2));
            Assert.True(_additionalClasses.GetNotAddedStudentsFromGroup(group3).Contains(student3));
        }
    }
}