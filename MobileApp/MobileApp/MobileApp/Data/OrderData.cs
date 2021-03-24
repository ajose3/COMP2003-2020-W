using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Data
{
    class OrderData
    {
        public static IList<Order> Orders { get; private set; }

        static OrderData()
        {
            Orders = new List<Order>();
            // NOTE: make sure each product has a unique id

            Orders.Add(new Order
            {
                Id = 1,
                Name = "TV",
                Description = "This is a TV",
                Price = 350.00f,
                ImageUrl = "https://brain-images-ssl.cdn.dixons.com/9/5/10191559/u_10191559.jpg",
                Stock = 100,
                Date = new DateTime(2020,12,1),
                Quantity = 2
            });
            Orders.Add(new Order
            {
                Id = 2,
                Name = "Phone",
                Description = "This is a Phone",
                Price = 500.00f,
                ImageUrl = "https://www.popsci.com/resizer/6iA2dK-qrizE_TrGloIM5mYz5Mw=/760x570/filters:focal(600x450:601x451)/arc-anglerfish-arc2-prod-bonnier.s3.amazonaws.com/public/VYHDQWEYQJMUBJTKNV4MMC5KMU.jpg",
                Stock = 100,
                Date = new DateTime(2020, 11, 1),
                Quantity = 1
            });
            Orders.Add(new Order
            {
                Id = 3,
                Name = "Mouse",
                Description = "This is a Mouse",
                Price = 10.25f,
                ImageUrl = "https://snpi.dell.com/snp/images/products/large/en-uk~570-AAMH/570-AAMH.jpg",
                Stock = 100,
                Date = new DateTime(2020, 10, 1),
                Quantity = 4
            });
            Orders.Add(new Order
            {
                Id = 4,
                Name = "TV2",
                Description = "This is a TV2",
                Price = 190.00f,
                ImageUrl = "https://m.media-amazon.com/images/I/81L55-A04BL._AC_UY218_.jpg",
                Stock = 10000,
                Date = new DateTime(2021, 1, 10),
                Quantity = 1
            });

        }
    }
}
