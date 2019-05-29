using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models
{
    public class Prices
    {
        public string RelatedCoin { get; set; }

        public double? LastPrice { get; set; }

        public double? HighPrice { get; set; }

        public double? LowPrice { get; set; }

        public double? BuyPrice { get; set; }

        public double? SellPrice { get; set; }
    }
}