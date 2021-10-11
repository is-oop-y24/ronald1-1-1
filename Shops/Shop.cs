using System;
using System.Collections.Generic;
using Shops.Tools;

namespace Shops
{
    public class Shop
    {
        private string _address;
        private string _name;
        private int _id;
        private float _money;

        private Dictionary<Product, ProductDescription> _products;

        public Shop(string address, string name)
            : this(address, name, 0) { }

        public Shop(string address, string name, int id)
        {
            _address = address;
            _name = name;
            _id = id;
            _products = new Dictionary<Product, ProductDescription>();
        }

        public string Name => _name;
        public float Money => _money;

        public void AddProduct(Product product, int count, float cost)
        {
            if (_products.ContainsKey(product))
            {
                ProductDescription productDescription = _products[product];
                productDescription.Cost = cost;
                productDescription.ChangeCount(count);
            }
            else
            {
                _products.Add(product, new ProductDescription(count, cost));
            }
        }

        public ProductDescription FindProduct(Product good)
        {
            if (_products.ContainsKey(good))
            {
                return _products[good];
            }
            else
            {
                return null;
            }
        }

        public void ChangeCost(Product product, float cost)
        {
            if (_products.ContainsKey(product))
            {
                var productDescription = _products[product];
                productDescription.Cost = cost;
            }
            else
            {
                throw new ShopsException("That good not found:" + product.Name);
            }
        }

        public void BuyProducts(Product product, int count)
        {
            if (_products.ContainsKey(product))
            {
                _products[product].ChangeCount(-count);
                _money += _products[product].Cost * count;
            }
            else
            {
                throw new ShopsException("That good not found:" + product.Name);
            }
        }
    }
}