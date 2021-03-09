using _2.SwaggerAPI.Service.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerAPI.Service.CalendarService;
using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI.AdminApp.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class ShiftController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ICalendarService _calendarService;

        public ShiftController(IAdminService adminService, ICalendarService calendarService)
        {
            _adminService = adminService;
            _calendarService = calendarService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ListDoctor = await _calendarService.GetAllDoctor();
            ViewBag.ListRoom = await _calendarService.GetAllRoom();
            ViewBag.ListTime = await _calendarService.GetAllTime();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> New(string RoomId, string DoctorId, DateTime Date, string TimeId)
        {
            var result = await _calendarService.RegisterCalendar(
                new ViewModel.CalendarRequest.CalendarRequest { 
                    RoomId = RoomId,
                    DoctorId = DoctorId,
                    Date = Date,
                    TimeId = TimeId
                });

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllShift()
        {
            var shifts = await _calendarService.GetAllShift();
            var rooms = await _calendarService.GetAllRoom();
            var times = await _calendarService.GetAllTime();

            List<FullCalendarEvent> fullCalendarEvents = new List<FullCalendarEvent>();

            foreach (var shift in shifts)
            {
                string tenPhong = "";
                string gio = "";

                foreach (var room in rooms)
                {
                    if (shift.RoomId == room.id)
                    {
                        tenPhong = room.name;
                    }
                }

                foreach (var time in times)
                {
                    if (shift.TimeId == time.id)
                    {
                        if (time.id == "T01" || time.id == "T02" || time.id == "T03")
                        {
                            var s = time.shiftTime.Split(" - ");
                            gio = "0" + s[0] + " - " + "0" + s[1];
                        }
                        else if (time.id == "T04")
                        {
                            var s = time.shiftTime.Split(" - ");
                            gio = "0" + s[0] + " - " + s[1];
                        }
                        else 
                        {
                            gio = time.shiftTime;
                        }
                    }
                }

                FullCalendarEvent fullCalendar = new FullCalendarEvent
                {
                    id = shift.Id,
                    title = "KB " + tenPhong.ToLower().Replace("khám ", ""),
                    start = shift.Date.ToString("yyyy-MM-dd") + "T" + gio.Split(" - ")[0] + ":00",
                    end = shift.Date.ToString("yyyy-MM-dd") + "T" + gio.Split(" - ")[1] + ":00"
                };
                fullCalendarEvents.Add(fullCalendar);
            }

            return Json(fullCalendarEvents);
        }

        [HttpGet]
        public async Task<JsonResult> Delete(string shiftID)
        {
            var result = await _adminService.DeleteShift(shiftID);
            return Json(result);
        }
    }
}
