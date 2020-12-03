using System;
using System.Collections.Generic;

#nullable disable

namespace AdminInterface.Models
{
    public partial class Product
    {
        public Product()
        {
            Ratings = new HashSet<Rating>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public string TypeOfProduct { get; set; }
        public int? AmountSold { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
