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

namespace Botcoin.Controllers.Api
{
    public class BotcoinController : ApiController
    {
        private const string MBBaseUrl = @"https://www.mercadobitcoin.net/tapi/v3/";

        private static BotcoinConfig Botcoin = new BotcoinConfig()
        {
            TotalBalance = new Balance()
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
            else if (options.ActionName == ActionNames.ConnectTapi && !string.IsNullOrWhiteSpace(options.TapiId)
                && !string.IsNullOrWhiteSpace(options.TapiKey) && !string.IsNullOrWhiteSpace(options.TapiMethod))
            {
                var result = await ConnectTapiAsync(options);
                return Ok(result);
            }
            else if (options.ActionName == ActionNames.SetConfig)
            {
                SetConfig(options);
                return Ok();
            }

            return NotFound();
        }

        #region ActionMethods

        private void SetConfig(BotcoinOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.TapiId))
                Botcoin.TapiId = options.TapiId;
            if (!string.IsNullOrWhiteSpace(options.TapiKey))
                Botcoin.TapiKey = options.TapiKey;
            if (options.IsConnected.HasValue)
                Botcoin.IsConnected = options.IsConnected.Value;
            if (options.TotalBalance != null)
            {
                if (options.TotalBalance.BRL.HasValue)
                    Botcoin.TotalBalance.BRL = options.TotalBalance.BRL;
                if (options.TotalBalance.BTC.HasValue)
                    Botcoin.TotalBalance.BTC = options.TotalBalance.BTC;
                if (options.TotalBalance.BCH.HasValue)
                    Botcoin.TotalBalance.BCH = options.TotalBalance.BCH;
                if (options.TotalBalance.LTC.HasValue)
                    Botcoin.TotalBalance.LTC = options.TotalBalance.LTC;
            }
        }

        private async Task<string> GetPricesAsync(BotcoinOptions options)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(@"https://www.mercadobitcoin.net/api/" + options.SelectedCoin + @"/ticker/");

                var response = await client.GetAsync(uri);

                var result = await response.Content.ReadAsStringAsync();

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
