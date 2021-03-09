using _2.SwaggerAPI.Service.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerAPI.ViewModel.AdminViewModel;
using SwaggerAPI.ViewModel.Request;
using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI.AdminApp.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class PatientEaminationListController : Controller
    {
        private readonly IAdminService _adminService;
        public PatientEaminationListController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AdminViewModel viewModel = new AdminViewModel();

            var listRoom = await _adminService.GetAllRoom();
            viewModel.listRooms = listRoom;

            var listShift = await _adminService.GetAllShift();
            viewModel.listRequests = listShift;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdminViewModel request)
        {
            AdminViewModel viewModel = new AdminViewModel();

            var result = await _adminService.GetAllShiftByRoomId(request.Room);
            var listRoom = await _adminService.GetAllRoom();
            viewModel.listRooms = listRoom;
            viewModel.listRequests = result;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> ChangeStatus(string id, int status)
        {
            var result = await _adminService.changeSchedules(id, status);
            return Json(result);
        }

        public async Task<IActionResult> RoomSelection()
        {
            var listRoom = await _adminService.GetAllRoom();
            var listShift = await _adminService.GetAllShift();
            ViewBag.listRoom = listRoom;
            ViewBag.listShift = listShift;

            return View();
        }

        [Route("{controller}/Examination/{id}")]
        public async Task<IActionResult> Examination(string id)
        {
            var roomName = "";
            var doctor = "";
            var result = await _adminService.GetAllShiftByShiftId(id);
            if (result.Count > 0)
            {
                roomName = result[0].Room;
                doctor = result[0].Doctor;

                foreach(var schedule in result)
                {
                    if (schedule.Status == 1)
                    {
                        await _adminService.changeSchedules(schedule.Id, 2);
                        schedule.Status = 2;
                    }
                }
            }

            ViewBag.roomName = roomName;
            ViewBag.doctor = doctor;
            ViewBag.To = result.Count;

            return View(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetShiftsByRoom(string roomID)
        {
            string[] arrT = { "8:00 - 8:30", "8:30 - 9:00", "9:00 - 9:30", "9:30 - 10:00", "10:00 - 10:30", "10:30 - 11:00", "12:00 - 12:30", "12:30 - 13:00", "13:00 - 13:30", "13:30 - 14:00", "14:00 - 14:30", "14:30 - 15:00", "15:00 - 15:30", "15:30 - 16:00", "16:00 - 16:30", "16:30 - 17:00" };
            var shifts = await _adminService.GetShiftsByRoom(roomID);

            foreach(var shift in shifts)
            {
                shift.TimeId = shift.Date.ToString("dd/MM/yyyy") + " (" + arrT[getInt(shift.TimeId) - 1] + ")";
            }

            return Json(shifts);
        }

        private static int getInt(string a)
        {
            string b = string.Empty;
            int val = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }

            if (b.Length > 0)
                val = int.Parse(b);

            return Convert.ToInt32(val);
        }
    }
}
