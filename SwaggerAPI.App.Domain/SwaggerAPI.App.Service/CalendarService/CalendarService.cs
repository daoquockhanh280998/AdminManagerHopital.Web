using SwaggerAPI.Service.CalendarService;
using Newtonsoft.Json;
using SwaggerAPI.ViewModel.CalendarRequest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SwaggerAPI.ViewModel.Response;

namespace SwaggerAPI.App.Domain
{
    public class CalendarService : ICalendarService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CalendarService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<DoctorRequestModel>> GetAllDoctor()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://192.168.1.13:8080");

            var response = await client.GetAsync($"/api/Doctor/all-doctors");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .Value<JArray>("data")
                .ToObject<List<DoctorRequestModel>>();

            return h;
        }

        public async Task<List<RoomRequestModel>> GetAllRoom()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://192.168.1.13:8080");

            var response = await client.GetAsync($"/api/Shift/all-rooms");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .Value<JArray>("data")
                .ToObject<List<RoomRequestModel>>();

            return h;
        }

        public async Task<List<ShiftResponse>> GetAllShift()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://192.168.1.13:8080");

            var response = await client.GetAsync($"/api/Shift/all-shifts");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .Value<JArray>("data")
                .ToObject<List<ShiftResponse>>();

            return h;
        }

        public async Task<List<TimeRequestModel>> GetAllTime()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://192.168.1.13:8080");

            var response = await client.GetAsync($"/api/Shift/all-times");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .Value<JArray>("data")
                .ToObject<List<TimeRequestModel>>();

            return h;
        }

        public async Task<bool> RegisterCalendar(CalendarRequest calendar)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://192.168.1.13:8080");

            var json = JsonConvert.SerializeObject(calendar);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Shift/add-shift", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }


    }
}
