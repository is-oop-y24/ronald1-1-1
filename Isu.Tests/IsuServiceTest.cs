using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3123");
            Student student = _isuService.AddStudent( group, "Grisha");
            if(!student.Group.Equals(group)) Assert.Fail();
            if(!group.Students.Contains(student)) Assert.Fail();
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group group = _isuService.AddGroup("M3123");
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i <= 25; i++)
                {
                    _isuService.AddStudent(group, "Grisha");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() => _isuService.AddGroup("M1343") );
            Assert.Catch<IsuException>(() => _isuService.AddGroup("M31d4") );
            Assert.Catch<IsuException>(() => _isuService.AddGroup("M341") );
            Assert.Catch<IsuException>(() => _isuService.AddGroup("M3641") );
            Assert.Catch<IsuException>(() => _isuService.AddGroup("M3041") );
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group1 = _isuService.AddGroup("M3111");
            Group group2 = _isuService.AddGroup("M3222");
            Student student = _isuService.AddStudent(group1, "Grisha");
            _isuService.ChangeStudentGroup(student, group2);
            if(!student.Group.Equals(group2)) Assert.Fail();
            if(group1.Students.Contains(student)) Assert.Fail();
            if(!group2.Students.Contains(student)) Assert.Fail();
        }
    }
}