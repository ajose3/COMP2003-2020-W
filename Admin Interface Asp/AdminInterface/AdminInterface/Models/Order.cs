using System;
using System.Collections.Generic;

#nullable disable

namespace AdminInterface.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime DateTime { get; set; }
        public int Quantity { get; set; }
    }
}
