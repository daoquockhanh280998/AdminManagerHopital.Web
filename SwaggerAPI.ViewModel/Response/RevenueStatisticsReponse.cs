using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Response
{
    public class RevenueStatisticsReponse
    {
        [JsonProperty("cardNumber")]
        public int cardNumber { get; set; }
        [JsonProperty("action")]
        public string action { get; set; }
        [JsonProperty("amount")]
        public int amount { get; set; }
        [JsonProperty("date")]
        public DateTime date { get; set; }
    }
}
