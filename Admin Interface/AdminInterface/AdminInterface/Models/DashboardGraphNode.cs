using System;
namespace AdminInterface.Models
{
    public class DashboardGraphNode
    {
        public DashboardGraphNode(int theTotalSales, int theMonth)
        {
            totalSales = theTotalSales;
            month = theMonth;
        }

        public int totalSales { get; set; }
        public int month { get; set; }
    }
}
