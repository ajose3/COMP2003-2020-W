using System;
using System.Collections.Generic;

namespace AdminInterface.Models
{
    public partial class Products
    {
        public Products()
        {
            Orders = new HashSet<Orders>();
            Reviews = new HashSet<Reviews>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
        public int TotalSold { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double? AvgRating { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
