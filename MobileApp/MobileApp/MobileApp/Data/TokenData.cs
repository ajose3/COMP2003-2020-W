﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Data
{
    public class TokenData
    {
        TokenData() { value = "0"; }
        public static string value { get; set; }

        //public static TokenData FromJson(string json) => JsonConvert.DeserializeObject<TokenData>(json);

    }
}
