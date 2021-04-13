using System;
using System.Collections.Generic;
using MobileApp.Models;

namespace MobileApp.Data
{
    public static class Basket
    {
        public static List<Product> Products = new List<Product>();

        public static void AddProduct(Product product)
        {
            Products.Add(product);
        }
        public static void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }

        public static float GetTotalPrice()
        {
            float totalPrice = 0;

            foreach (var Product in Products)
            {
                totalPrice += Product.Price;
            }

            return totalPrice;
        }

        public static void Clear()
        {
            Products.Clear();
        }
        public static List<Product> loadBasket()
        {
            List<Product> inBasket = new List<Product>();
            foreach (var prod in Products)
            {
                inBasket.Add(prod);
            }
            return inBasket;
        }
    }
}
