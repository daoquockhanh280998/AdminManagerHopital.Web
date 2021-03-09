using _2.SwaggerAPI.Service.AdminService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SwaggerAPI.Common;
using SwaggerAPI.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SwaggerAPI.ViewModel.ApiResult;
using SwaggerAPI.ViewModel.Response;
using SwaggerAPI.ViewModel.AdminViewModel;

namespace SwaggerAPI.AdminApp.Controllers
{
    public class UserController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IAdminService _adminService;
        public UserController(IAdminService adminService, IConfiguration configuration)
        {
            _adminService = adminService;
            _configuration = configuration;
        }
        [HttpGet, Route("Login")]
        public async Task<IActionResult> Login()
        {
            ViewBag.MessageType = "primary";
            ViewBag.Message = "Mời Đăng Nhập";
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();

        }

        public async Task<ActionResult> Index()
        {
            var result = await _adminService.GetAllUsers();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewUserRequest request)
        {
            request.IsApprove = true;
            try
            {
                var result = await _adminService.AddNewUser(request);

                ViewBag.Message = "Tạo mới Người dùng:" +
                        "<br />ID: " + result.Id +
                        "<br />Họ tên: " + result.LastName + " " + result.FirstName +
                        "<br />GT: " + (result.Gender ? "Nam" : "Nữ") +
                        "<br />Ngày sinh: " + result.Birthday.ToString("dd-MM-yyyy") +
                        "<br />Địa chỉ: " + result.Address +
                        "<br />SĐT: " + result.Phone +
                        "<br />Username: " + result.Username +
                        "<br />Phân quyền: " + result.Role;
                ViewBag.MessageType = "Success";
            }
            catch
            {
                ViewBag.Message = "Tạo mới người dùng thất bại";
                ViewBag.MessageType = "Danger";
            }
            return View();
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
             var result = await _adminService.Authenticate(request);
          

            if (result == "Username or password is incorrect")
            {
                ViewBag.MessageType = "danger";
                ViewBag.Message = result;
                return View();
            }
            else if (result == "Account has been blocked")
            {
                ViewBag.MessageType = "danger";
                ViewBag.Message = result;
                return View();
            }

            var userPrincipal = this.ValidateJwtToken(result);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };
            //HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);


            //var  principal = await HttpContext.User.Identity.


            //data.AddClaim(new Claim(ClaimTypes.UserData, user.Email));
            return RedirectToAction("Index","Home");
        }


        [HttpPost, Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
      
        private ClaimsPrincipal ValidateJwtToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = "http://myaudience1.com";
            validationParameters.ValidIssuer = "http://mysite1.com";
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }

        [HttpPut]
        public async Task<JsonResult> Update(UserResponse user)
        {
            var result = await _adminService.UpdateUser(user);
            return Json(result);
        }
    }
}
