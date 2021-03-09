using _2.SwaggerAPI.Service.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerAPI.ViewModel.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI.AdminApp.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class ScheduleController : Controller
    {
        private readonly IAdminService _adminService;
        public ScheduleController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string[] arrT = { "8:00 - 8:30", "8:30 - 9:00", "9:00 - 9:30", "9:30 - 10:00", "10:00 - 10:30", "10:30 - 11:00", "12:00 - 12:30", "12:30 - 13:00", "13:00 - 13:30", "13:30 - 14:00", "14:00 - 14:30", "14:30 - 15:00", "15:00 - 15:30", "15:30 - 16:00", "16:00 - 16:30", "16:30 - 17:00" };
            ViewBag.Message = null;
            var listTime = await _adminService.GetAllTimeShift();

            ScheduleViewModel viewModel = new ScheduleViewModel();

            foreach(var time in listTime)
            {
                time.TimeId = arrT[getInt(time.TimeId) - 1];
            }

            viewModel.ShiftTimeResponses = listTime;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ScheduleViewModel schedule)
        {
            var result = await _adminService.CreateSchedule(schedule);
            if (result == true)
            {
                ViewBag.MessageType = "Success";
                ViewBag.Message = "Đăng Ký Thành Công";
            }
            else
            {
                ViewBag.MessageType = "Danger";
                ViewBag.Message = "Đăng Ký Không Thành Công";
            }

            string[] arrT = { "8:00 - 8:30", "8:30 - 9:00", "9:00 - 9:30", "9:30 - 10:00", "10:00 - 10:30", "10:30 - 11:00", "12:00 - 12:30", "12:30 - 13:00", "13:00 - 13:30", "13:30 - 14:00", "14:00 - 14:30", "14:30 - 15:00", "15:00 - 15:30", "15:30 - 16:00", "16:00 - 16:30", "16:30 - 17:00" };

            var listTime = await _adminService.GetAllTimeShift();

            ScheduleViewModel viewModel = new ScheduleViewModel();

            foreach (var time in listTime)
            {
                time.TimeId = arrT[getInt(time.TimeId) - 1];
            }

            viewModel.ShiftTimeResponses = listTime;
            return View(viewModel);
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
