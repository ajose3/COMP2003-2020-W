using Newtonsoft.Json;
using System;

namespace MobileApp.Models
{
    public class Product
    {
        [JsonProperty("productId")]
        public int Id { get; set; }

        [JsonProperty("productName")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("stock")]
        public int Stock { get; set; }
        //[JsonProperty("totalSold")]
        //public int TotalSold { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }

        //[JsonProperty("avgRating")]
        //public string AvgRating { get; set; }




        public Product()
        {
        }

        public Product(int theId, string theName, string theDescription, float thePrice, string theImageUrl, int theStock)
        {
            Id = theId;
            Name = theName;
            Description = theDescription;
            Price = thePrice;
            ImageUrl = theImageUrl;
            Stock = theStock;
        }

        public Product(BasketProduct product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            ImageUrl = product.ImageUrl;
            Stock = product.Stock;
        }
    }
}