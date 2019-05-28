using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botcoin.Models.Responses
{
    public class GetAccountInfoResponseData
    {
        public BalanceSet balance { get; set; }

        public WithdrawalLimits withdrawal_limits { get; set; }
    }

    public class BalanceSet
    {
        public Brl brl { get; set; }

        public Btc btc { get; set; }

        public Bch bch { get; set; }

        public Ltc ltc { get; set; }

        public Eth eth { get; set; }

        public Xrp xrp { get; set; }
    }

    public class WithdrawalLimits
    {
        public Brl brl { get; set; }

        public Btc btc { get; set; }

        public Bch bch { get; set; }

        public Ltc ltc { get; set; }

        public Eth eth { get; set; }

        public Xrp xrp { get; set; }
    }

    public class Brl
    {
        public string available { get; set; }

        public string total { get; set; }

        public int? amount_open_orders { get; set; }
    }

    public class Btc
    {
        public string available { get; set; }

        public string total { get; set; }

        public int? amount_open_orders { get; set; }
    }

    public class Bch
    {
        public string available { get; set; }

        public string total { get; set; }

        public int? amount_open_orders { get; set; }
    }

    public class Ltc
    {
        public string available { get; set; }

        public string total { get; set; }

        public int? amount_open_orders { get; set; }
    }

    public class Eth
    {
        public string available { get; set; }

        public string total { get; set; }

        public int? amount_open_orders { get; set; }
    }

    public class Xrp
    {
        public string available { get; set; }

        public string total { get; set; }

        public int? amount_open_orders { get; set; }
    }
}