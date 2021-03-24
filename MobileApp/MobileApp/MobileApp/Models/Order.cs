using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Order : Product
    {
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public float Total { get; set; }

        public Order()
        {
            getTotal();
        }

        public Order(int theId, string theName, string theDescription, float thePrice, string theImageUrl, int theStock, int theQuantity)
        {
            Id = theId;
            Name = theName;
            Description = theDescription;
            Price = thePrice;
            ImageUrl = theImageUrl;
            Stock = theStock;

            Quantity = theQuantity;
            Date = DateTime.Now;
            Total = getTotal();
        }


        public float getTotal()
        {
            return Price * Quantity;
        }
    }

}
