using System;
using System.Collections.Generic;

#nullable disable

namespace _2003_Web_API.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime TimeOrdered { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }

        //public virtual Customer Customer { get; set; }
        //public virtual Product Product { get; set; }
    }
}
