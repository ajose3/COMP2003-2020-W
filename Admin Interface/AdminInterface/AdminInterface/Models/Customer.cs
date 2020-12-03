using System;
using System.Collections.Generic;

#nullable disable

namespace AdminInterface.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Ratings = new HashSet<Rating>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
