using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class API_Product
    {
        /*public Product()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }*/

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
        public int TotalSold { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double? AvgRating { get; set; }

        /*public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }*/
    }
}
