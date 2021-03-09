using SwaggerAPI.ViewModel.CalendarRequest;
using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerAPI.Service.CalendarService
{
    public interface ICalendarService
    {
        Task<bool> RegisterCalendar(CalendarRequest calendarRequest);
        Task<List<DoctorRequestModel>> GetAllDoctor();
        Task<List<RoomRequestModel>> GetAllRoom();
        Task<List<TimeRequestModel>> GetAllTime();
        Task<List<ShiftResponse>> GetAllShift();
    }
}
