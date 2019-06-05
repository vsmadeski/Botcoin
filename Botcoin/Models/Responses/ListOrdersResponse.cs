using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models.Responses
{
    public class ListOrdersResponse
    {
        public ListOrdersResponseData response_data { get; set; }

        public int status_code { get; set; }

        public string error_message { get; set; }

        public string server_unix_timestamp { get; set; }
    }
}