using _2.SwaggerAPI.Service.AdminService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwaggerAPI.ViewModel.AdminViewModel;
using SwaggerAPI.ViewModel.ApiResult;
using SwaggerAPI.ViewModel.CalendarRequest;
using SwaggerAPI.ViewModel.PatientViewModel;
using SwaggerAPI.ViewModel.Request;
using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerAPI.App.Domain.SwaggerAPI.App.Service.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AdminService(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


        //public async Task<bool> changeSchedules(string id, int status)
        //{
        //    ChangeScheduleStatusRequest request = new ChangeScheduleStatusRequest()
        //    {
        //        id = id,
        //        status = status
        //    };
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri("http://192.168.1.13:8080");

        //    var json = JsonConvert.SerializeObject(request);
        //    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await client.PostAsync($"/api/Shift/change-schedule-status", httpContent);
        //    var result = await response.Content.ReadAsStringAsync();

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public async Task<bool> CreateSchedule(ScheduleViewModel schedule)
        {

            ScheduleRequest request = new ScheduleRequest()
            {
                name = schedule.name,
                birthday = schedule.birthday,
                address = schedule.address,
                bhyt = schedule.bhyt,
                gender = schedule.gender,
                patientId = schedule.patientId,
                phone = schedule.phone,
                shiftId = schedule.shiftId
            };


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Shift/request-new-schedule", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        public async Task<List<InvoicesViewModel>> GetAllInvoices()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/all-invoices");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<InvoicesViewModel>>();

            return h;
        }

        public async Task<List<DoctorRequestModel>> GetAllDoctors()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Doctor/all-doctors");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result").Value<JArray>("data")
                .ToObject<List<DoctorRequestModel>>();

            return h;
        }

        public async Task<List<RoomRequestModel>> GetAllRoom()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/all-rooms");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .Value<JArray>("data")
                .ToObject<List<RoomRequestModel>>();
            return h;
        }

        public async Task<List<PatientEaminationListRequest>> GetAllShift()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/all-schedule-responses");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<PatientEaminationListRequest>>();

            return h;
        }

        public async Task<List<PatientEaminationListRequest>> GetAllShiftByRoomId(string roomID)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/all-schedule-responses?roomId={roomID}");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<PatientEaminationListRequest>>();

            return h;
        }

        public async Task<List<PatientEaminationListRequest>> GetAllShiftByShiftId(string shiftID)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/all-schedule-responses-by-shift?shiftId={shiftID}");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<PatientEaminationListRequest>>();

            return h;
        }

        public async Task<List<ShiftResponse>> GetShiftsByRoom(string roomID)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/get-shifts-by-room?roomID={roomID}");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result").Value<JArray>("data")
                .ToObject<List<ShiftResponse>>();

            return h;
        }

        public async Task<List<ShiftResponse>> GetAllTimeShift()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/all-shifts");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .Value<JArray>("data")
                .ToObject<List<ShiftResponse>>();

            return h;
        }

        public async Task<ChangeScheduleStatusResponse> changeSchedules(string id, int status)
        {
            ChangeScheduleStatusRequest request = new ChangeScheduleStatusRequest()
            {
                id = id,
                status = status
            };
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Shift/change-schedule-status", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result").ToObject<ChangeScheduleStatusResponse>();

            return h;
        }

        public async Task<InvoicesViewModel> ChangeInvoiceStatus(string id, int status)
        {
            ChangeScheduleStatusRequest request = new ChangeScheduleStatusRequest()
            {
                id = id,
                status = status
            };
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Shift/change-invoice-status", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result").ToObject<InvoicesViewModel>();

            return h;
        }

        public async Task<List<PatientViewModel>> GetAllPatients()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Patient/get-all-patients");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result").Value<JArray>("data")
                .ToObject<List<PatientViewModel>>();

            return h;
        }

        public async Task<DoctorRequestModel> AddNewDoctor(NewDoctorRequest request)
        {

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Doctor/add-new-doctor", httpContent);
            var result = await response.Content.ReadAsStringAsync();


            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .ToObject<DoctorRequestModel>();

            return h;
        }

        public async Task<RoomRequestModel> AddNewRoom(string roomName)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/add-new-room?roomName={roomName}");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .ToObject<RoomRequestModel>();

            return h;
        }

        public async Task<PatientViewModel> RegistrationPatientInformation(RegistrationInformationRequest request)
        {

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Patient/add-new-patient", httpContent);
            var result = await response.Content.ReadAsStringAsync();


            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .ToObject<PatientViewModel>();

            return h;
        }
        public async Task<List<UserResponse>> GetAllUsers()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://192.168.1.13:8080");

            var response = await client.GetAsync($"/api/User/all-users");
            var result = await response.Content.ReadAsStringAsync();

            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JArray>("result")
                .ToObject<List<UserResponse>>();

            return h;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
           
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PostAsync($"/api/User/authenticate", httpContent);
            var token = await response.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<UserResponse> AddNewUser(NewUserRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/User/add-new-user", httpContent);
            var result = await response.Content.ReadAsStringAsync();


            var o = JsonConvert.DeserializeObject<JObject>(result);
            var h = o.Value<JObject>("result")
                .ToObject<UserResponse>();

            return h;
        }

        public async Task<bool> UpdateRoom(RoomRequestModel request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Shift/update-room", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDoctor(DoctorRequestModel request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Doctor/update-doctor", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePatient(PatientViewModel request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Patient/update-patient", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUser(UserResponse request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/User/update-user", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteShift(string shiftID)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/api/Shift/delete-shifts?shiftID={shiftID}");
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }
    }
}
