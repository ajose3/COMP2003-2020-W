using System;
using System.Collections.Generic;
using MobileApp.Models;

namespace MobileApp.Data
{
    public static class Basket
    {
        //public static List<Product> Products = new List<Product>();
        public static List<CheckOutProduct> CheckProds = new List<CheckOutProduct>();

        public static void AddProduct(Product product)
        {
            //Products.Add(product);
            bool isFound = false;
            if (CheckProds.Count == 0)
            {
                CheckOutProduct checkoutProd = new CheckOutProduct(product, 1);
                CheckProds.Add(checkoutProd);
            }
            else
            {
                foreach (var checkProd in CheckProds)
                {
                    if (checkProd.Id == product.Id)
                    {
                        checkProd.Quantity = checkProd.Quantity + 1;
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    CheckOutProduct checkoutProd = new CheckOutProduct(product, 1);
                    CheckProds.Add(checkoutProd);
                }
            }
        }
        public static void RemoveProduct(Product product)
        {
            //Products.Remove(product);

            foreach (var prod in CheckProds)
            {
                if (prod.Id == product.Id)
                {
                    if (prod.Quantity <= 1)
                    {
                        CheckProds.Remove(prod);
                    }
                    else
                    {
                        prod.Quantity = prod.Quantity - 1;
                    }
                    break;
                }
            }
        }

        public static float GetTotalPrice()
        {
            float totalPrice = 0;

            foreach (var Product in CheckProds)
            {
                if (Product.Quantity > 1)
                {
                    totalPrice += Product.Price * Product.Quantity;
                }
                else
                {
                    totalPrice += Product.Price;
                }
            }

            return totalPrice;
        }

        public static int GetNumItem()
        {
            int num = 0;

            foreach (var Product in CheckProds)
            {
                if (Product.Quantity > 1)
                {
                    num += Product.Quantity;
                }
                else
                {
                    num += 1;
                }
            }

            return num;
        }

        public static void Clear()
        {
            //Products.Clear();
            CheckProds.Clear();
        }
        //public static List<Product> loadBasket()
        //{
        //    List<Product> inBasket = new List<Product>();
        //    foreach (var prod in Products)
        //    {
        //        inBasket.Add(prod);
        //    }
        //    return inBasket;
        //}

        public static List<CheckOutProduct> loadNewBasket()
        {
            List<CheckOutProduct> inBasket = new List<CheckOutProduct>();
            foreach (var prod in CheckProds)
            {
                inBasket.Add(prod);
            }
            return inBasket;
        }


        //public static List<CheckOutProduct> LoadCheckout()
        //{
        //    List<CheckOutProduct> products = new List<CheckOutProduct>();


        //    return products;
        //}
    }
}
