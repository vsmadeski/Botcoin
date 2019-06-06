using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models.Responses
{
    public class ListOrderbookResponseData
    {
        public Orderbook orderbook { get; set; }
    }

    public class Orderbook
    {
        public Bid[] bids { get; set; }

        public Ask[] asks { get; set; }

        public long latest_order_id { get; set; }
    }

    public class Bid
    {
        public long order_id { get; set; }

        public string quantity { get; set; }

        public string limit_price { get; set; }

        public bool is_owner { get; set; }
    }

    public class Ask
    {
        public long order_id { get; set; }

        public string quantity { get; set; }

        public string limit_price { get; set; }

        public bool is_owner { get; set; }
    }
}