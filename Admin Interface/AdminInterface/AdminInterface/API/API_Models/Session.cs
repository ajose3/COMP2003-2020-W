using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class API_Session
    {
        public int SessionId { get; set; }
        public int CustomerId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
