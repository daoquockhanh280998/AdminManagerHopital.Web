using SwaggerAPI.ViewModel.AdminViewModel;
using SwaggerAPI.ViewModel.CalendarRequest;
using SwaggerAPI.ViewModel.PatientViewModel;
using SwaggerAPI.ViewModel.Request;
using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _2.SwaggerAPI.Service.AdminService
{
    public interface IAdminService
    {
        Task<List<PatientEaminationListRequest>> GetAllShiftByRoomId(string roomID);
        Task<List<PatientEaminationListRequest>> GetAllShiftByShiftId(string shiftID);
        Task<List<PatientEaminationListRequest>> GetAllShift();
        Task<List<RoomRequestModel>> GetAllRoom();
        Task<List<ShiftResponse>> GetAllTimeShift();
        Task<List<ShiftResponse>> GetShiftsByRoom(string roomID);
        Task<bool> CreateSchedule(ScheduleViewModel schedule);

        Task<List<InvoicesViewModel>> GetAllInvoices();

        Task<DoctorRequestModel> AddNewDoctor(NewDoctorRequest request);

        Task<RoomRequestModel> AddNewRoom(string roomName);

        Task<List<DoctorRequestModel>> GetAllDoctors();

        Task<ChangeScheduleStatusResponse> changeSchedules(string id , int status);


        Task<InvoicesViewModel> ChangeInvoiceStatus(string id, int status);

        Task<List<PatientViewModel>> GetAllPatients();

        Task<PatientViewModel> RegistrationPatientInformation(RegistrationInformationRequest  request);
        Task<string> Authenticate(LoginRequest request);

        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse> AddNewUser(NewUserRequest request);
        Task<bool> UpdateRoom(RoomRequestModel request);
        Task<bool> UpdateDoctor(DoctorRequestModel request);
        Task<bool> UpdatePatient(PatientViewModel request);
        Task<bool> UpdateUser(UserResponse request);
        Task<bool> DeleteShift(string shiftID);
    }
}
