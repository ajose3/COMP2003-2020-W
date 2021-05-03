﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class BasketProduct : Product
    {
        [PrimaryKey,AutoIncrement]
        public int BasketId { get; set; }
        public int Quantity { get; set; }
        public BasketProduct() { }

        public BasketProduct(int id, string name, string description, float price, string imageUrl, int stock, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            Stock = stock;
            Quantity = quantity;
        }

        public BasketProduct(Product product, int quantity)
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