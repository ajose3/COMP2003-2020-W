using System;
using System.Collections.Generic;

namespace AdminInterface.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public DateTime TimeOrdered { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Products Product { get; set; }
    }
}
