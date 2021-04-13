using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class OrdersGroup
    {
        //public OrdersGroup()
        //{
        //    Remaining = getRemaining();
        //}
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public List<Order> theOrders { get; set; }
        public int Remaining { get; set; }
    //public string[] Images { get; set; }
        public int getRemaining()
        {
            if(theOrders.Count > 4)
            {
                return theOrders.Count - 4;
            }
            else
            {
                return 0;
            }
        }
    }
}
