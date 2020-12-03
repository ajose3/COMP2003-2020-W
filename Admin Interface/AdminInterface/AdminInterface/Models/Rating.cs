using System;
using System.Collections.Generic;

#nullable disable

namespace AdminInterface.Models
{
    public partial class Rating
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public byte Rating1 { get; set; }
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
