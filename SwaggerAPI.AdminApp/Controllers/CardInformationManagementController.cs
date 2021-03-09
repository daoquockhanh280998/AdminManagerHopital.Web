using _2.SwaggerAPI.Service.CardManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI.AdminApp.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class CardInformationManagementController : Controller
    {
        private readonly ICardService _cardService;

        public CardInformationManagementController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listCard = await _cardService.GetALLCard();
            return View(listCard);
        }
    }
}
