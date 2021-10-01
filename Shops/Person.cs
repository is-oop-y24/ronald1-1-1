namespace Shops
{
    public class Person
    {
        private string _name;
        private float _money;

        public Person(string name)
            : this(name, 0) { }

        public Person(string name, float money)
        {
            _money = money;
            _name = name;
        }

        public float Money
        {
            get => _money;
            set => _money = value;
        }

        public string Name => _name;
    }
}