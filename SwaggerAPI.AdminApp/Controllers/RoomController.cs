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
  //  [Authorize(Roles = "Admin")]
    public class RoomController : Controller
    {
        private readonly IAdminService _adminService;
        public RoomController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _adminService.GetAllRoom();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewRoomRequest request)
        {
            try
            {
                var result = await _adminService.AddNewRoom(request.roomName);

                ViewBag.Message = "Tạo mới Phòng khám:" +
                        "<br />ID: " + result.id +
                        "<br />Tên Phòng: " + result.name;
                ViewBag.MessageType = "Success";
            }
            catch
            {
                ViewBag.Message = "Tạo mới phòng khám thất bại";
                ViewBag.MessageType = "Danger";
            }
            return View();
        }

        [HttpPut]
        public async Task<JsonResult> Update(RoomRequestModel room)
        {
            var result = await _adminService.UpdateRoom(room);
            return Json(result);
        }
    }
}
