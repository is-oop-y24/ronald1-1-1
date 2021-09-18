namespace Isu
{
    public class Student
    {
        private Group _group;
        private string _name;
        private int _id;

        public Student(Group group, string name)
            : this(group, name, 0) { }
        public Student(Group group, string name, int id)
        {
            _group = group;
            _name = name;
            _id = id;
        }

        public int Id => _id;
        public string Name => _name;

        public Group Group
        {
            get => _group;
            set { _group = value; }
        }
    }
}