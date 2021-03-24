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

        public float getTotal()
        {
            return Price * Quantity;
        }
    }

}
