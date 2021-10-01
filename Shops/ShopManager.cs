using System;
using System.Collections.Generic;
using Shops.Tools;

namespace Shops
{
    public class ShopManager
    {
        private int _shopId;
        private List<Shop> _shops;

        public ShopManager()
        {
            _shops = new List<Shop>();
            _shopId = 0;
        }

        public Shop CreateShop(string name, string address)
        {
            var shop = new Shop(address, name, ++_shopId);
            _shops.Add(shop);
            return shop;
        }

        public Shop FindCheapestShopForProduct(Product product, int count)
        {
            Shop cheapestShop = null;
            float cheapestCost = 0;
            foreach (var shop in _shops)
            {
                var productDescription = shop.FindProduct(product);
                if (productDescription != null)
                {
                    if (productDescription.Count >= count)
                    {
                        if (cheapestShop == null || cheapestCost > productDescription.Cost)
                        {
                            cheapestShop = shop;
                            cheapestCost = productDescription.Cost;
                        }
                    }
                }
            }

            if (cheapestShop == null)
            {
                throw new ShopsException("That good not found:" + product.Name);
            }

            return cheapestShop;
        }

        public void ChangeCostInShop(Shop shop, Product product, float cost)
        {
            shop.ChangeCost(product, cost);
        }

        public void BuyProductGroup(Person person, Shop shop, List<Tuple<Product, int>> products)
        {
            foreach (Tuple<Product, int> product in products)
            {
                try
                {
                    BuyProducts(person, product.Item1, shop, product.Item2);
                }
                catch (ShopsException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public Shop FindCheapestShopForProductGroup(List<Tuple<Product, int>> products)
        {
            Shop cheapestShop = null;
            float cheapestCost = 0;
            foreach (var shop in _shops)
            {
                float cost = -1;
                foreach (Tuple<Product, int> product in products)
                {
                    ProductDescription productDescription = shop.FindProduct(product.Item1);
                    if (productDescription != null)
                    {
                        if (productDescription.Count >= product.Item2)
                        {
                            cost += productDescription.Cost * product.Item2;
                        }
                        else
                        {
                            cost = -1;
                            break;
                        }
                    }
                    else
                    {
                        cost = -1;
                        break;
                    }
                }

                if (cost != -1)
                {
                    if (cheapestShop == null || cheapestCost > cost)
                    {
                        cheapestShop = shop;
                        cheapestCost = cost;
                    }
                }
            }

            return cheapestShop;
        }

        public void BuyProducts(Person person, Product product, Shop shop, int count)
        {
            var productDescription = shop.FindProduct(product);
            if (productDescription.Count < count)
            {
                throw new ShopsException("There are not so many products:" + count);
            }

            if (productDescription.Cost * count > person.Money)
            {
                throw new ShopsException("Person doesn't have enough money:" + person.Name);
            }

            person.Money -= count * productDescription.Cost;
            shop.BuyProducts(product, count);
        }

        public void AddProductToShop(Shop shop, Product product, int count, float cost)
        {
            shop.AddProduct(product, count, cost);
        }
    }
}