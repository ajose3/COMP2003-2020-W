using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Data
{
    public static class ProductData
    {
        public static IList<Product> Products { get; private set; }

        static ProductData()
        {
            Products = new List<Product>();
            Products.Add(new Product
            {
                Id = 1,
                Name = "TV",
                Description = "This is a TV",
                Price = 55
            });
            Products.Add(new Product
            {
                Id = 2,
                Name = "Phone",
                Description = "This is a Phone",
                Price = 55
            }); 
            Products.Add(new Product
            {
                Id = 3,
                Name = "Mouse",
                Description = "This is a Mouse",
                Price = 55
            });
        }
    }
}
