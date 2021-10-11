#nullable enable
namespace Shops
{
    public class Product
    {
        private string _name;
        private int _id;

        public Product(string name, int id)
        {
            _name = name;
            _id = id;
        }

        public string Name => _name;
        public override int GetHashCode()
        {
            return _id;
        }
    }
}