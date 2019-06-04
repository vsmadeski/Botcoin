using System;
using Botcoin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Botcoin.Utils.Static;
using System.Security.Cryptography;
using Botcoin.Models.Responses;

namespace Botcoin.Controllers.Api
{
    public class BotcoinController : ApiController
    {
        private const string MBBaseUrl = @"https://www.mercadobitcoin.net/tapi/v3/";

        private static BotcoinConfig Botcoin = new BotcoinConfig()
        {
            TotalBalance = new Balance(),
            OpsBalance = new Balance(),
            ReservedBalance = new Balance(),
            Prices = new Prices()
        };

        [HttpGet]
        public async Task<IHttpActionResult> ExecuteAction(string botcoinParams)
        {
            var options = JsonConvert.DeserializeObject<BotcoinOptions>(botcoinParams);

            if(options.ActionName == ActionNames.GetPrices && !string.IsNullOrWhiteSpace(options.SelectedCoin))
            {
                var result = await GetPricesAsync(options);
                return Ok(result);
            }
            else if (options.ActionName == ActionNames.ConnectTapi && !string.IsNullOrWhiteSpace(options.TapiId) && !string.IsNullOrWhiteSpace(options.TapiKey) && !string.IsNullOrWhiteSpace(options.TapiMethod))
            {
                var result = await ConnectTapiAsync(options);
                return Ok(result);
            }
            else if (options.ActionName == ActionNames.SetOpsBalance && Botcoin.IsConnected.Value && options.OpsBalance != null && !string.IsNullOrWhiteSpace(options.SelectedCoin))
            {
                try
                {
                    SetOpsBalance(options);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);       
                }
            }
            else if (options.ActionName == ActionNames.Activate && Botcoin.IsConnected.Value && Botcoin.IsBalanceSet.Value)
            {
                ActivateBotcoin(options);
                return Ok();
            }
            else if (options.ActionName == ActionNames.BotcoinMain && Botcoin.IsActive.Value)
            {

            }

            return NotFound();
        }

        #region ActionMethods

        private void SetOpsBalance(BotcoinOptions options)
        {
            if(options.OpsBalance.BRL > Botcoin.TotalBalance.BRL
                || options.OpsBalance.BTC > Botcoin.TotalBalance.BTC
                || options.OpsBalance.BCH > Botcoin.TotalBalance.BCH
                || options.OpsBalance.LTC > Botcoin.TotalBalance.LTC)
            {
                throw new Exception("Saldo de operação não pode ser maior do que o saldo total.");
            }

            Botcoin.OpsBalance.BRL = options.OpsBalance.BRL;
            Botcoin.OpsBalance.BTC = options.OpsBalance.BTC;
            Botcoin.OpsBalance.BCH = options.OpsBalance.BCH;
            Botcoin.OpsBalance.LTC = options.OpsBalance.LTC;
            Botcoin.ReservedBalance.BRL = Botcoin.TotalBalance.BRL - Botcoin.OpsBalance.BRL;
            Botcoin.ReservedBalance.BTC = Botcoin.TotalBalance.BTC - Botcoin.OpsBalance.BTC;
            Botcoin.ReservedBalance.BCH = Botcoin.TotalBalance.BCH - Botcoin.OpsBalance.BCH;
            Botcoin.ReservedBalance.LTC = Botcoin.TotalBalance.LTC - Botcoin.OpsBalance.LTC;
            Botcoin.SelectedCoin = options.SelectedCoin;
            Botcoin.IsBalanceSet = true;
        }

        private void ActivateBotcoin(BotcoinOptions options)
        {
            Botcoin.IsActive = true;
        }

        private async Task<string> GetPricesAsync(BotcoinOptions options)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(@"https://www.mercadobitcoin.net/api/" + options.SelectedCoin + @"/ticker/");

                var response = await client.GetAsync(uri);

                var result = await response.Content.ReadAsStringAsync();

                var jsonData = JsonConvert.DeserializeObject<GetPricesResponse>(result);

                Botcoin.Prices.BuyPrice = double.Parse(jsonData.ticker.buy, System.Globalization.CultureInfo.InvariantCulture);
                Botcoin.Prices.SellPrice = double.Parse(jsonData.ticker.sell, System.Globalization.CultureInfo.InvariantCulture);
                Botcoin.Prices.HighPrice = double.Parse(jsonData.ticker.high, System.Globalization.CultureInfo.InvariantCulture);
                Botcoin.Prices.LowPrice = double.Parse(jsonData.ticker.low, System.Globalization.CultureInfo.InvariantCulture);
                Botcoin.Prices.LastPrice = double.Parse(jsonData.ticker.last, System.Globalization.CultureInfo.InvariantCulture);
                Botcoin.Prices.RelatedCoin = options.SelectedCoin;

                return result;
            }
        }

        private async Task<string> ConnectTapiAsync(BotcoinOptions options)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(MBBaseUrl);

                var parameters = new Dictionary<string, string>
                {
                    {"tapi_method", options.TapiMethod },
                    {"tapi_nonce", DateTime.Now.Ticks.ToString() }
                };

                var content = new FormUrlEncodedContent(parameters);

                var paramsEncoded = await content.ReadAsStringAsync();

                var tapiMac = GenerateTapiMac(options.TapiKey, paramsEncoded);

                content.Headers.Add("TAPI-ID", options.TapiId);
                content.Headers.Add("TAPI-MAC", tapiMac);

                var respone = await client.PostAsync(uri, content);

                var result = await respone.Content.ReadAsStringAsync();

                var jsonData = JsonConvert.DeserializeObject<GetAccountInfoResponse>(result);

                if (jsonData.status_code == 100)
                {
                    var culture = System.Globalization.CultureInfo.InvariantCulture;
                    Botcoin.TapiId = options.TapiId;
                    Botcoin.TapiKey = options.TapiKey;
                    Botcoin.IsConnected = true;
                    Botcoin.TotalBalance.BRL = double.Parse(jsonData.response_data.balance.brl.available, culture);
                    Botcoin.TotalBalance.BTC = double.Parse(jsonData.response_data.balance.btc.available, culture);
                    Botcoin.TotalBalance.BCH = double.Parse(jsonData.response_data.balance.bch.available, culture);
                    Botcoin.TotalBalance.LTC = double.Parse(jsonData.response_data.balance.ltc.available, culture);
                }

                return result;
            }
        }

        #endregion

        #region Utils
        private string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("x2"); // hex format
            }
            return (sbinary);
        }

        private string GenerateTapiMac(string key, string paramsEncoded)
        {
            string message = @"/tapi/v3/?" + paramsEncoded;

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            byte[] keyByte = encoding.GetBytes(key);
            HMACSHA512 hmacsha512 = new HMACSHA512(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashmessage = hmacsha512.ComputeHash(messageBytes);
            string hmac5 = ByteToString(hashmessage);

            return hmac5;
        }
        #endregion
    }
}
