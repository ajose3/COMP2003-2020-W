using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Order : Product
    {
        //OrderId
        //    timeOrdered
        //    productId
        //    customer Id

        public int Quantity { get; set; }
        [JsonProperty("timeOrdered")]
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool displayBtn { get; set; } // binded to to toggle button visablility ->

        public float Total { get; set; }

        public Order()
        {
            //getTotal();
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
            OrderDate = DateTime.Now;
            //add random number of days to order date
            var rand = new Random();
            DeliveryDate = DateTime.Now.AddDays(rand.Next(31)); 
            Total = getTotal();
            displayBtn = false;
        }


        public float getTotal()
        {
            return Price * Quantity;
        }

    }

}
