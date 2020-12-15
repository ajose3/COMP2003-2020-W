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
                Price = 350,
                ImageUrl = "https://brain-images-ssl.cdn.dixons.com/9/5/10191559/u_10191559.jpg"
            });
            Products.Add(new Product
            {
                Id = 2,
                Name = "Phone",
                Description = "This is a Phone",
                Price = 500,
                ImageUrl = "https://www.popsci.com/resizer/6iA2dK-qrizE_TrGloIM5mYz5Mw=/760x570/filters:focal(600x450:601x451)/arc-anglerfish-arc2-prod-bonnier.s3.amazonaws.com/public/VYHDQWEYQJMUBJTKNV4MMC5KMU.jpg"
            }); 
            Products.Add(new Product
            {
                Id = 3,
                Name = "Mouse",
                Description = "This is a Mouse",
                Price = 10,
                ImageUrl = "https://snpi.dell.com/snp/images/products/large/en-uk~570-AAMH/570-AAMH.jpg"
            });
        }
    }
}
