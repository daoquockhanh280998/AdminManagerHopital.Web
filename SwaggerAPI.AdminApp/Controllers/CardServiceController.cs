using _2.SwaggerAPI.Service.CardManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerAPI.ViewModel.CalendarRequest;
using SwaggerAPI.ViewModel.CardManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SwaggerAPI.AdminApp.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class CardServiceController : Controller
    {
        private readonly ICardService _cardService;

        public CardServiceController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckDervice dervice)
        {
            

            ViewBag.MessageType = "Success";
           
            if (dervice.NameService != "Vui Lòng Chọn Dịch Vụ ")
            {
                if (dervice.NameService == "create")
                {
                    try
                    {
                        CreateCard createCard = new CreateCard
                        {
                            Money = dervice.Money,
                            patientId = dervice.patientId,
                            CreatedBy = User.Claims.Where(e => e.Type == "UserName").Select(e => e.Value).SingleOrDefault()
                    };

                        var create = await _cardService.Create(createCard);

                        ViewBag.Message = "Tạo mới thẻ thành công:" +
                                "<br />ID: " + create.id +
                                "<br />Mã thẻ: " + create.cardNumber +
                                "<br />Số dư: " + string.Format("{0:n0}", create.money) + " VND" +
                                "<br />Mã bệnh nhân: " + create.patientId +
                                "<br />Ngày tạo: " + create.createdDate.ToString("dd-MM-yyyy") +
                                "<br />Ngày hết hạn: " + create.expiredDate.ToString("dd-MM-yyyy");
                    }
                    catch
                    {
                        ViewBag.Message = "Tạo mới thẻ thất bại";
                        ViewBag.MessageType = "Danger";
                    }
                }
                if (dervice.NameService == "top-up")
                {
                    try
                    {
                        TopUp topUp = new TopUp
                        {
                            cardNumber = dervice.cardNumber,
                            money = dervice.Money,
                            CreatedBy = User.Identity.Name
                        };

                        var t = await _cardService.TopUp(topUp);

                        ViewBag.Message = "Nạp thành công " + string.Format("{0:n0}", topUp.money) + " VND vào thẻ" +
                                "<br />ID: " + t.id +
                                "<br />Mã thẻ: " + t.cardNumber +
                                "<br />Số dư mới: " + string.Format("{0:n0}", t.money) + " VND" +
                                "<br />Mã bệnh nhân: " + t.patientId +
                                "<br />Ngày tạo: " + t.createdDate.ToString("dd-MM-yyyy") +
                                "<br />Ngày hết hạn: " + t.expiredDate.ToString("dd-MM-yyyy");
                    }
                    catch
                    {
                        ViewBag.Message = "Mã thẻ không tồn tại";
                        ViewBag.MessageType = "Danger";
                    }
                }
                if (dervice.NameService == "return" || dervice.NameService == "block")
                {
                    try
                    {
                        ChangeCardStatusRequest changeCardStatusRequest = new ChangeCardStatusRequest
                        {
                            cardNumber = dervice.cardNumber,
                            status = dervice.NameService == "return" ? "RETURNED" : "BLOCKED",
                            CreatedBy = User.Identity.Name
                        };
                        var t = await _cardService.ChangeCardStatus(changeCardStatusRequest);

                        ViewBag.Message = (dervice.NameService == "return" ? "Trả" : "Khóa") +
                                " thành công thẻ " + dervice.cardNumber +
                                "<br />ID: " + t.id +
                                "<br />Mã thẻ: " + t.cardNumber +
                                "<br />Số dư mới: " + string.Format("{0:n0}", t.money) + " VND" +
                                "<br />Mã bệnh nhân: " + t.patientId +
                                "<br />Ngày tạo: " + t.createdDate.ToString("dd-MM-yyyy") +
                                "<br />Ngày hết hạn: " + t.expiredDate.ToString("dd-MM-yyyy") +
                                "<br />Trạng thái: " + t.status;
                    }
                    catch
                    {
                        ViewBag.Message = "Mã thẻ không tồn tại";
                        ViewBag.MessageType = "Danger";
                    }
                }
                if (dervice.NameService == "change")
                {
                    try
                    {
                        ChangeCardStatusRequest changeCardStatusRequest = new ChangeCardStatusRequest
                        {
                            cardNumber = dervice.cardNumber,
                            status = "CHANGED",
                            CreatedBy = User.Identity.Name
                        };
                        var t = await _cardService.ChangeCardStatus(changeCardStatusRequest);

                        ViewBag.Message = "Đổi thành công thẻ " + dervice.cardNumber +
                                "<br />ID mới: " + t.id +
                                "<br />Mã thẻ mới: " + t.cardNumber +
                                "<br />Số dư mới: " + string.Format("{0:n0}", t.money) + " VND" +
                                "<br />Mã bệnh nhân: " + t.patientId +
                                "<br />Ngày tạo mới: " + t.createdDate.ToString("dd-MM-yyyy") +
                                "<br />Ngày hết hạn mới: " + t.expiredDate.ToString("dd-MM-yyyy") +
                                "<br />Trạng thái: " + t.status == "1" ? "Ok" : t.status == "2" ? "CHANGED" : t.status == "3" ? "RETURNED" : "BLOCKED";
                    }
                    catch
                    {
                        ViewBag.Message = "Mã thẻ không tồn tại";
                        ViewBag.MessageType = "Danger";
                    }
                }
                if (dervice.NameService == "search")
                {
                    try
                    {
                        PatientCardRequest patientCardRequest = new PatientCardRequest
                        {
                            cardNumber = dervice.cardNumber,
                            patientId = "",
                            CreatedBy = User.Identity.Name
                        };
                        var t = await _cardService.GetCardInformation(patientCardRequest);

                        ViewBag.Message = "Thông tin " + (!string.IsNullOrEmpty(t.PatientId) ? "chủ " : "") + "thẻ " + dervice.cardNumber + (!string.IsNullOrEmpty(t.PatientId) ?
                                "<br />ID bệnh nhân: " + t.PatientId +
                                "<br />Tên bệnh nhân: " + t.Name +
                                "<br />Điện thoại: " + t.Phone +
                                "<br />Địa chỉ: " + t.Address +
                                "<br />Ngày sinh: " + t.Birthday.ToString("dd-MM-yyyy") +
                                "<br />Giới tính: " + (t.Gender ? "Nam" : "Nữ") : "") +
                                "<br />Mã thẻ: " + t.CardNumber +
                                "<br />Số dư: " + string.Format("{0:n0}", t.Money) + " VND" +
                                "<br />Ngày tạo: " + t.CreatedDate.ToString("dd-MM-yyyy") +
                                "<br />Ngày hết hạn: " + t.ExpiredDate.ToString("dd-MM-yyyy") +
                                "<br />Trạng thái: " + (t.CardStatus == 1 ? "Ok" : t.CardStatus == 2 ? "CHANGED" : t.CardStatus == 3 ? "RETURNED" : "BLOCKED");
                    }
                    catch
                    {
                        ViewBag.Message = "Mã thẻ không tồn tại, <a href='#' id='btn-create-card' style='text-decoration:none;'>tạo thẻ</a>?";
                        ViewBag.MessageType = "Warning";
                    }
                }
            }

            return View();
        }
    }
}
