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
        //public string Remaining => getRemaining();

        //public string getRemaining()
        //{
        //    if(theOrders.Count > 4)
        //    {
        //        string Remaining = (theOrders.Count - 4).ToString();
        //        return Remaining + "+";
        //    }
        //    else
        //    {
        //        return "0";
        //    }
        //}
    }
}
