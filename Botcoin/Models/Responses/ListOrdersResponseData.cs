using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models.Responses
{
    public class ListOrdersResponseData
    {
        public Order[] orders { get; set; }
    }

    public class Order
    {
        public long order_id { get; set; }

        public string coin_pair { get; set; }

        public int order_type { get; set; }

        public int status { get; set; }

        public bool has_fills { get; set; }

        public double quantity { get; set; }

        public double limit_price { get; set; }

        public double executed_quantity { get; set; }

        public double executed_price_avg { get; set; }

        public double fee { get; set; }

        public long created_timestamp { get; set; }

        public long updated_timestamp { get; set; }

        public Operation[] operations { get; set; }
    }

    public class Operation
    {
        public long operation_id { get; set; }

        public double quantity { get; set; }

        public double price { get; set; }

        public double fee_rate { get; set; }

        public long executed_timestamp { get; set; }
    }
}