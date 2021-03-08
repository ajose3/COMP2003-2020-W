using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Review
    {
        public int ProdID { get; set; }
        public int CustomerID { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }

        public Review()
        {
        }

        public string StarColor1 { get; set; }
        public string StarColor2 { get; set; }
        public string StarColor3 { get; set; }
        public string StarColor4 { get; set; }
        public string StarColor5 { get; set; }

    }
}
