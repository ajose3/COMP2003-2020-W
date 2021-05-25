using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Review
    {
        [JsonProperty("productID")]
        public int ProdID { get; set; }
        [JsonProperty("customerId")]
        public int CustomerID { get; set; }
        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Desciption { get; set; }

        public Review()
        {
        }

        public Review(int prodID, int rating, string title, string desciption)
        {
            ProdID = prodID;
            Rating = rating;
            Title = title;
            Desciption = desciption;
        }

        public string StarColor1 { get; set; }
        public string StarColor2 { get; set; }
        public string StarColor3 { get; set; }
        public string StarColor4 { get; set; }
        public string StarColor5 { get; set; }

    }
}
