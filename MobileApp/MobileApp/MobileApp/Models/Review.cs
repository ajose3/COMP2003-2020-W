using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProdID { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
    }
}
