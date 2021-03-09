using _2.SwaggerAPI.Service.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerAPI.ViewModel.PatientViewModel;
using SwaggerAPI.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI.AdminApp.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class PatientController : Controller
    {
        private readonly IAdminService _adminService;
        public PatientController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _adminService.GetAllPatients();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistrationInformationRequest request)
        {
            try
            {
                var result = await _adminService.RegistrationPatientInformation(request);

                ViewBag.Message = "Tạo mới Bệnh Nhân:" +
                        "<br />ID: " + result.id +
                        "<br />Tên Bệnh Nhân: " + result.name +
                        "<br />Ngày Sinh: " + result.birthday.ToString("dd-MM-yyyy") +
                        "<br />Giới Tính " + (result.gender == true ? "Nam" : "Nữ") +
                        "<br />Số Điện Thoại: " + result.phone +
                        "<br />Địa Chỉ: " + result.address;
                ViewBag.MessageType = "Success";
            }
            catch
            {
                ViewBag.Message = "Tạo mới bệnh nhân thất bại";
                ViewBag.MessageType = "Danger";
            }
            return View();
        }

        [HttpPut]
        public async Task<JsonResult> Update(PatientViewModel patient)
        {
            var result = await _adminService.UpdatePatient(patient);
            return Json(result);
        }
    }
}
