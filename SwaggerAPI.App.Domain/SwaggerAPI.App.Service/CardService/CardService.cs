using _2.SwaggerAPI.Service.CardManager;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwaggerAPI.ViewModel.AdminViewModel;
using SwaggerAPI.ViewModel.CalendarRequest;
using SwaggerAPI.ViewModel.CardManager;
using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerAPI.App.Domain.SwaggerAPI.App.Service.CardService
{
    public class CardService : ICardService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CardService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


        public async Task<CardReponse> Create(CreateCard createCard)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(createCard);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Card/create", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .ToObject<CardReponse>();

            return h;
        }

        public async Task<CardReponse> TopUp(TopUp topUp)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(topUp);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Card/top-up", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .ToObject<CardReponse>();

            return h;
        }

        public async Task<CardReponse> ChangeCardStatus(ChangeCardStatusRequest changeCardStatusRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(changeCardStatusRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Card/change-card-status", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .ToObject<CardReponse>();

            return h;
        }

        public async Task<List<RevenueStatisticsReponse>> RevenueStatisticsReponse()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Card/get-revenue-statistics");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<RevenueStatisticsReponse>>();

            return h;
        }

        public async Task<PatientCardResponse> GetCardInformation(PatientCardRequest patientCardRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(patientCardRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Card/get-full-info", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .ToObject<PatientCardResponse>();

            return h;
        }

        public async Task<List<RevenueStatisticsReponse>> GetRevenueStatisticsByAction(string action)
        {

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Card/get-revenue-statistic-by-action?action={action}");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<RevenueStatisticsReponse>>();

            return h;
        }

        public async Task<List<RevenueStatisticsReponse>> GetRevenueStatisticsByDate(DateTime dateTime)
        {

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Card/get-revenue-statistic-by-date?date={dateTime.Date}");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<RevenueStatisticsReponse>>();

            return h;
        }

        public async Task<List<RevenueStatisticsReponse>> GetRevenueStatisticsByActionAndDate(string action, DateTime dateTime)
        {

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Card/get-revenue-statistic-by-action-and-date?action={action}&date={dateTime.Date}");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<RevenueStatisticsReponse>>();

            return h;
        }

        public async Task<List<CardInformationViewModel>> GetALLCard()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Card/all");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
               .Value<JArray>("data")
               .ToObject<List<CardInformationViewModel>>();

            return h;
        }
    }
}
