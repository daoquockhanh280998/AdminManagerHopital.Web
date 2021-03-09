using SwaggerAPI.Service.CalendarService;
using Microsoft.AspNetCore.Mvc;
using SwaggerAPI.ViewModel.CalendarRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwaggerAPI.ViewModel.Response;
using System.Text.Json;

namespace swaggerAPI.Calendar.Controllers
{

    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;
        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet, Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.ListDoctor = await _calendarService.GetAllDoctor();
            ViewBag.ListRoom = await _calendarService.GetAllRoom();
            ViewBag.ListTime = await _calendarService.GetAllTime();
            return View();
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(CalendarRequest calendar)
        {
            var result = await _calendarService.RegisterCalendar(calendar);
         
            if (result)
                return Content("Thêm thành công!");

            return Content("Thêm thất bại!");
        }

        public async Task<IActionResult> GetAllDoctors()
        {
            var result = await _calendarService.GetAllDoctor();

            return View(result);
        }

        public async Task<IActionResult> CreateFullCalendar()
        {
            ViewBag.ListDoctor = await _calendarService.GetAllDoctor();
            ViewBag.ListRoom = await _calendarService.GetAllRoom();
            ViewBag.ListTime = await _calendarService.GetAllTime();
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllShift()
        {
            var shifts = await _calendarService.GetAllShift();
            var rooms = await _calendarService.GetAllRoom();
            var times = await _calendarService.GetAllTime();

            List <FullCalendarEvent> fullCalendarEvents = new List<FullCalendarEvent>();

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
                    title = "Khám bệnh " + tenPhong.ToLower(),
                    start = shift.Date.ToString("yyyy-MM-dd") + "T" + gio.Split(" - ")[0] + ":00",
                    end = shift.Date.ToString("yyyy-MM-dd") + "T" + gio.Split(" - ")[1] + ":00"
                };
                fullCalendarEvents.Add(fullCalendar);
            }

            return Json(fullCalendarEvents);
        }

        //[HttpPost]
        //public async Task<JsonResult> CreateNewShift([FromBody] CalendarRequest calendar)
        //{
        //    var result = await _calendarService.RegisterCalendar(calendar);

        //    if (result)
        //        return Json(true);

        //    return Json(false);
        //}
    }
}
