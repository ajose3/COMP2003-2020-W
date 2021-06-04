using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class API_Order : API_Product
    {
        public int OrderId { get; set; }
        public DateTime TimeOrdered { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
        //public int ProductId { get; set; }
        public int CustomerId { get; set; }




        //public virtual Customer Customer { get; set; }
        //public virtual Product Product { get; set; }
    }
}
