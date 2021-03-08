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
            //getStarColors();
        }

        public string StarColor1 { get; set; }
        public string StarColor2 { get; set; }
        public string StarColor3 { get; set; }
        public string StarColor4 { get; set; }
        public string StarColor5 { get; set; }

        //public void getStarColors()
        //{
        //    StarColor1 = "Grey";
        //    StarColor2 = "Grey";
        //    StarColor3 = "Grey";
        //    StarColor4 = "Grey";
        //    StarColor5 = "Grey";
        //    if (Rating >= 1)
        //    {
        //        StarColor1 = "Gold";
        //    }
        //    if (Rating >= 2)
        //    {
        //        StarColor2 = "Gold";
        //    }
        //    if (Rating >= 3)
        //    {
        //        StarColor3 = "Gold";
        //    }
        //    if (Rating >= 4)
        //    {
        //        StarColor4 = "Gold";
        //    }
        //    if (Rating >= 5)
        //    {
        //        StarColor5 = "Gold";
        //    }
        //}
    }
}
