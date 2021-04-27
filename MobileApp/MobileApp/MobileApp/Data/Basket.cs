using System;
using System.Collections.Generic;
using MobileApp.Models;

namespace MobileApp.Data
{
    public static class Basket
    {
        public static List<CheckOutProduct> Products = new List<CheckOutProduct>();
        public static void AddProduct(Product product)
        {
            //Products.Add(product);
            bool isFound = false;
            if (Products.Count == 0)
            {
                CheckOutProduct checkoutProd = new CheckOutProduct(product, 1);
                Products.Add(checkoutProd);
            }
            else
            {
                foreach (var checkProd in Products)
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
                    Products.Add(checkoutProd);
                }
            }
        }
        public static void RemoveProduct(Product product)
        {
            //Products.Remove(product);

            foreach (var prod in Products)
            {
                if (prod.Id == product.Id)
                {
                    if (prod.Quantity <= 1)
                    {
                        Products.Remove(prod);
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

            foreach (var Product in Products)
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

            foreach (var Product in Products)
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
            Products.Clear();
        }

        public static List<CheckOutProduct> loadBasket()
        {
            List<CheckOutProduct> inBasket = new List<CheckOutProduct>();
            foreach (var prod in Products)
            {
                inBasket.Add(prod);
            }
            return inBasket;
        }
    }
}
