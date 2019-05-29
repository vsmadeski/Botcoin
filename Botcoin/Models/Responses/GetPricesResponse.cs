using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models.Responses
{
    public class GetPricesResponse
    {
        public Ticker ticker { get; set; }
    }

    public class Ticker
    {
        public string buy { get; set; }

        public string sell { get; set; }

        public string high { get; set; }

        public string low { get; set; }

        public string vol { get; set; }

        public string last { get; set; }

        public long date { get; set; }
    }
}