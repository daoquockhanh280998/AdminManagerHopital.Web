using _2.SwaggerAPI.Service.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI.AdminApp.Controllers
{
 //   [Authorize(Roles = "Admin")]
    public class InvoicesController : Controller
    {
        private readonly IAdminService _adminService;
        public InvoicesController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _adminService.GetAllInvoices();
            return View(result);
        }

        [HttpPost]
        public async Task<JsonResult> ChangeStatus(string id, int status)
        {
            var result = await _adminService.ChangeInvoiceStatus(id, status);
            return Json(result);
        }
    }
}
