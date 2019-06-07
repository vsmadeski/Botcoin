using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models
{
    public class Balance
    {
        public decimal? BRL { get; set; }

        public decimal? BTC { get; set; }

        public decimal? BCH { get; set; }

        public decimal? LTC { get; set; }
    }
}