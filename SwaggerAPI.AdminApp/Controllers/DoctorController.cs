using _2.SwaggerAPI.Service.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerAPI.ViewModel.CalendarRequest;
using SwaggerAPI.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI.AdminApp.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly IAdminService _adminService;
        public DoctorController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _adminService.GetAllDoctors();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewDoctorRequest request)
        {
            try
            {
                var result = await _adminService.AddNewDoctor(request);

                ViewBag.Message = "Tạo mới Bác sĩ:" +
                        "<br />ID: " + result.id +
                        "<br />Tên Bác Sĩ: " + result.name +
                        "<br />Số Điện Thoại: " + result.phone +
                        "<br />Email: " + result.email;
                ViewBag.MessageType = "Success";
            }
            catch
            {
                ViewBag.Message = "Tạo mới bác sĩ thất bại";
                ViewBag.MessageType = "Danger";
            }
            return View();
        }

        [HttpPut]
        public async Task<JsonResult> Update(DoctorRequestModel doctor)
        {
            var result = await _adminService.UpdateDoctor(doctor);
            return Json(result);
        }
    }
}
