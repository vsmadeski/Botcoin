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

        public decimal quantity { get; set; }

        public decimal limit_price { get; set; }

        public decimal executed_quantity { get; set; }

        public decimal executed_price_avg { get; set; }

        public decimal fee { get; set; }

        public long created_timestamp { get; set; }

        public long updated_timestamp { get; set; }

        public Operation[] operations { get; set; }
    }

    public class Operation
    {
        public long operation_id { get; set; }

        public decimal quantity { get; set; }

        public decimal price { get; set; }

        public decimal fee_rate { get; set; }

        public long executed_timestamp { get; set; }
    }
}