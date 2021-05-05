using System;
using System.Collections.Generic;

namespace AdminInterface.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
            Reviews = new HashSet<Reviews>();
            Sessions = new HashSet<Sessions>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int? Age { get; set; }
        public bool? Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
        public virtual ICollection<Sessions> Sessions { get; set; }
    }
}
