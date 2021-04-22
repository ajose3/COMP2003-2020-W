using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class CheckOutProduct : Product
    {
        public int Quantity { get; set; }

        public CheckOutProduct(int id, string name, string description, float price, string imageUrl, int stock, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            Stock = stock;
            Quantity = quantity;
        }

        public CheckOutProduct(Product product, int quantity)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            ImageUrl = product.ImageUrl;
            Stock = product.Stock;
            Quantity = quantity;
        }
    }
}
