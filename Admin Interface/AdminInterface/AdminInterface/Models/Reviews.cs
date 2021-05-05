using System;
using System.Collections.Generic;

namespace AdminInterface.Models
{
    public partial class Reviews
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public byte Rating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Products Product { get; set; }
    }
}
