using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Tools;

namespace Shops.Tests
{
    public class ShopsTest
    {
        private ShopManager _manager;

        [SetUp]
        public void Setup()
        {
            _manager = new ShopManager();
        }

        [Test]
        public void AddProductAtShop_PersonCanBuyProduct()
        {
            var shop = _manager.CreateShop("1", "2");
            var product = new Product("Beer", 1235);
            _manager.AddProductToShop(shop, product, 5, 300);
            _manager.BuyProducts(new Person("grisha", 300), product, shop, 1);
            Assert.Catch<ShopsException>(() => _manager.BuyProducts(new Person("oleg", 2), product, shop, 1));
            Assert.Catch<ShopsException>(() => _manager.BuyProducts(new Person("lesha", 10000), product, shop, 5));
            if (shop.FindProduct(product).Count != 4)
            {
                Assert.Fail();
            }
        }      
        
        [Test]
        public void ChangeCost_CostWasChanged()
        {
            var shop = _manager.CreateShop("1", "2");
            var product = new Product("Beer", 8324);
            _manager.AddProductToShop(shop, product, 5, 300);
            _manager.ChangeCostInShop(shop, product, 200);
            if (shop.FindProduct(product).Cost != 200)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void FindCheapestProductGroup()
        {
            var shop1 = _manager.CreateShop("1", "1");
            var shop2 = _manager.CreateShop("2", "2");
            var shop3 = _manager.CreateShop("3", "3");
            var product1 = new Product("1", 1);
            var product2 = new Product("2", 2);
            var product3 = new Product("3", 3);
            
            _manager.AddProductToShop(shop1, product1, 1, 100);
            _manager.AddProductToShop(shop1, product2, 1, 200);
            _manager.AddProductToShop(shop1, product3, 3, 300);
            _manager.AddProductToShop(shop2, product1, 3, 200);
            _manager.AddProductToShop(shop2, product3, 1, 300);
            _manager.AddProductToShop(shop3, product1, 5, 300);
            _manager.AddProductToShop(shop3, product2, 5, 200);
            _manager.AddProductToShop(shop3, product3, 1, 100);
            var products = new List<Tuple<Product, int>>();
            products.Add(new Tuple<Product, int>(product1, 2));
            products.Add(new Tuple<Product, int>(product2, 1));
            products.Add(new Tuple<Product, int>(product3, 1));
            var shop = _manager.FindCheapestShopForProductGroup(products);

            if (!shop.Equals(shop3))
            {
                Assert.Fail();
            }
            
            var product4 = new Product("4", 4);
            Assert.Catch<ShopsException>(() => _manager.FindCheapestShopForProduct(product4, 4));
        }

        [Test]
        public void BuyProductGroup()
        {
            var shop1 = _manager.CreateShop("1", "1");
            var product1 = new Product("1", 1);
            var product2 = new Product("2", 2);
            var product3 = new Product("3", 3);
            
            _manager.AddProductToShop(shop1, product1, 1, 100);
            _manager.AddProductToShop(shop1, product2, 1, 200);
            _manager.AddProductToShop(shop1, product3, 3, 300);
            
            var products = new List<Tuple<Product, int>>();
            products.Add(new Tuple<Product, int>(product1, 1));
            products.Add(new Tuple<Product, int>(product2, 1));
            products.Add(new Tuple<Product, int>(product3, 2));

            var person = new Person("grisha", 1000);
            _manager.BuyProductGroup(person, shop1, products);
            if (!(person.Money == 100 && shop1.Money == 900))
            {
                Assert.Fail();
            }
        }
    }
    
}