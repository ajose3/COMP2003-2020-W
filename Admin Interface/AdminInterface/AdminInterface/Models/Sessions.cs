using System;
using System.Collections.Generic;

namespace AdminInterface.Models
{
    public partial class Sessions
    {
        public int SessionId { get; set; }
        public int CustomerId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
