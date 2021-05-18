using System;
using System.Collections.Generic;

#nullable disable

namespace _2003_Web_API.Models
{
    public partial class Customer
    {
        /*public Customer()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Sessions = new HashSet<Session>();
        }*/

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

        /*public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }*/
    }
}
