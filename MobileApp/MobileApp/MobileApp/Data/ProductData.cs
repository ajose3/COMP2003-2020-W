using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MobileApp.Data
{
    public static class ProductData
    {
        public static IList<Product> Products { get; private set; }

        static ProductData()
        {
            Products = new List<Product>();
            // NOTE: make sure each product has a unique id

            Products.Add(new Product
            {
                Id = 1,
                Name = "TV",
                Description = "This is a TV",
                Price = 350.00f,
                ImageUrl = "https://brain-images-ssl.cdn.dixons.com/9/5/10191559/u_10191559.jpg",
                Stock = 100
            });
            Products.Add(new Product
            {
                Id = 2,
                Name = "Phone",
                Description = "This is a Phone",
                Price = 500.00f,
                ImageUrl = "https://www.popsci.com/resizer/6iA2dK-qrizE_TrGloIM5mYz5Mw=/760x570/filters:focal(600x450:601x451)/arc-anglerfish-arc2-prod-bonnier.s3.amazonaws.com/public/VYHDQWEYQJMUBJTKNV4MMC5KMU.jpg",
                Stock = 100
            });
            Products.Add(new Product
            {
                Id = 3,
                Name = "Mouse",
                Description = "This is a Mouse",
                Price = 10.25f,
                ImageUrl = "https://snpi.dell.com/snp/images/products/large/en-uk~570-AAMH/570-AAMH.jpg",
                Stock = 100
            });
            Products.Add(new Product
            {
                Id = 4,
                Name = "TV2",
                Description = "This is a TV2",
                Price = 190.00f,
                ImageUrl = "https://m.media-amazon.com/images/I/81L55-A04BL._AC_UY218_.jpg",
                Stock = 10000
            });
            Products.Add(new Product
            {
                Id = 5,
                Name = "Console",
                Description = "This is a Console",
                Price = 500.00f,
                ImageUrl = "https://m.media-amazon.com/images/I/71PMC4DWWFL._AC_UY218_.jpg",
                Stock = 1
            });
            Products.Add(new Product
            {
                Id = 6,
                Name = "Phone2",
                Description = "This is a Phone2",
                Price = 600.00f,
                ImageUrl = "https://m.media-amazon.com/images/I/71efuy+3ZNL._AC_UY218_.jpg",
                Stock = 100
            });
            Products.Add(new Product
            {
                Id = 7,
                Name = "Charger",
                Description = "This is a Charger",
                Price = 10.00f,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51tWZFkT3AL._AC_SL1300_.jpg",
                Stock = 300
            });
        }

        public static Product GetTrending()
        {
            return Products[2];
        }

        public static Product GetFeatured()
        {
            return Products[4];
        }


    }

}
