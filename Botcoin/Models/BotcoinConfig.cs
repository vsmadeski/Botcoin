using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models
{
    public class BotcoinConfig
    {
        public string TapiId { get; set; }

        public string TapiKey { get; set; }

        public bool? IsConnected { get; set; }

        public Balance TotalBalance { get; set; }

        public Balance OpsBalance { get; set; }

        public string SelectedCoin { get; set; }

        public bool? IsBalanceSet { get; set; }
    }
}