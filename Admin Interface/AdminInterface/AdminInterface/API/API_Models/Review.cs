using System;
using System.Collections.Generic;

#nullable disable

namespace _2003_Web_API.Models
{
    public class Review
    {
        public int ProductID { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    /*public partial class Review
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public byte Rating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }*/
}
