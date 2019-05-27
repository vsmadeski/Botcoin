using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models
{
    public class BotcoinOptions
    {
        public string ActionName { get; set; }

        public string SelectedCoin { get; set; }

        public string TapiId { get; set; }

        public string TapiKey { get; set; }

        public string TapiMethod { get; set; }

        public bool? IsConnected { get; set; }

        public Balance TotalBalance { get; set; }

        public Balance OpsBalance { get; set; }

        public bool? IsBalanceSet { get; set; }
    }
}