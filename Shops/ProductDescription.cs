namespace Shops
{
    public class ProductDescription
    {
        private int _count;
        private float _cost;

        public ProductDescription(int count, float cost)
        {
            _count = count;
            _cost = cost;
        }

        public float Cost
        {
            get => _cost;
            set => _cost = value;
        }

        public int Count => _count;
        public void ChangeCount(int difference)
        {
            _count += difference;
        }
    }
}