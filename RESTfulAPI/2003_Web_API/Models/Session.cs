using System;
using System.Collections.Generic;

#nullable disable

namespace _2003_Web_API.Models
{
    public partial class Session
    {
        public int SessionId { get; set; }
        public int CustomerId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
