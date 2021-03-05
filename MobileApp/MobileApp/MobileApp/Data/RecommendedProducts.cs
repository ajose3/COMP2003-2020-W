using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Data
{
    class RecommendedProducts
    {
        public static IList<Product> RecProducts { get; private set; }
        static RecommendedProducts()
        {
            RecProducts = new List<Product>();

            RecProducts.Add(new Product
            {
                Id = 1,
                Name = "TV",
                Description = "This is a TV",
                Price = 350.00f,
                ImageUrl = "https://brain-images-ssl.cdn.dixons.com/9/5/10191559/u_10191559.jpg",
                Stock = 100
            });
            RecProducts.Add(new Product
            {
                Id = 4,
                Name = "TV2",
                Description = "This is a TV2",
                Price = 190.00f,
                ImageUrl = "https://m.media-amazon.com/images/I/81L55-A04BL._AC_UY218_.jpg",
                Stock = 10000
            });
            RecProducts.Add(new Product
            {
                Id = 5,
                Name = "Console",
                Description = "This is a Console",
                Price = 500.00f,
                ImageUrl = "https://m.media-amazon.com/images/I/71PMC4DWWFL._AC_UY218_.jpg",
                Stock = 1
            });
            RecProducts.Add(new Product
            {
                Id = 7,
                Name = "Charger",
                Description = "This is a Charger",
                Price = 10.00f,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51tWZFkT3AL._AC_SL1300_.jpg",
                Stock = 300
            });
        }

    }
}
