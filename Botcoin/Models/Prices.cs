using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models
{
    public class Prices
    {
        public string RelatedCoin { get; set; }

        public decimal? LastPrice { get; set; }

        public decimal? HighPrice { get; set; }

        public decimal? LowPrice { get; set; }

        public decimal? BuyPrice { get; set; }

        public decimal? SellPrice { get; set; }
    }
}