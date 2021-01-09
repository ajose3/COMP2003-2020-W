using System;

namespace MobileApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }

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
    }
}