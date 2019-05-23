using System;
using Botcoin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Botcoin.Utils;

namespace Botcoin.Controllers.Api
{
    public class BotcoinController : ApiController
    {
        public async Task<IHttpActionResult> GetPrices(string botcoinParams)
        {
            var options = JsonConvert.DeserializeObject<BotcoinOptions>(botcoinParams);

            if(options.ActionName == ActionNames.GetPrices && !string.IsNullOrWhiteSpace(options.SelectedCoin))
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(@"https://www.mercadobitcoin.net/api/" + options.SelectedCoin + @"/ticker/");

                    var response = await client.GetAsync(uri);

                    var result = await response.Content.ReadAsStringAsync();

                    return Ok(result);
                }
            }

            return NotFound();
        }
    }
}
