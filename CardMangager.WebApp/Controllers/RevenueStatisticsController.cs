using _2.SwaggerAPI.Service.CardManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SwaggerAPI.ViewModel.CardManager;
using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CardMangager.WebApp.Controllers
{
    public class RevenueStatisticsController : Controller
    {

        private readonly ICardService _cardService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RevenueStatisticsController(ICardService cardService, IHttpContextAccessor httpContextAccessor)
        {
            _cardService = cardService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _cardService.RevenueStatisticsReponse();
            CardCommon common = new CardCommon();
            common.RevenueStatistics = result;
            return View(common);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CardCommon common)
        {
            if (common.Action != "Index")
            {
                if (common.Action == "All")
                {
                    common.RevenueStatistics = await _cardService.RevenueStatisticsReponse();
                }
                if (common.Date != DateTime.MinValue && !string.IsNullOrEmpty(common.Action) && common.Action != "All")
                {
                    common.RevenueStatistics = await _cardService.GetRevenueStatisticsByActionAndDate(common.Action, common.Date);
                }
                else if (!string.IsNullOrEmpty(common.Action) && common.Date == DateTime.MinValue)
                {
                    common.RevenueStatistics = await _cardService.GetRevenueStatisticsByAction(common.Action);
                }
                else if (common.Date != DateTime.MinValue && (string.IsNullOrEmpty(common.Action) || common.Action != "All"))
                {
                    common.RevenueStatistics = await _cardService.GetRevenueStatisticsByDate(common.Date);
                }
            }

            return View(common);
        }

        [HttpPost]
        public async Task<JsonResult> ExportExcel(string Action, DateTime date)
        {
            var common = new CardCommon
            {
                Action = Action,
                Date = date
            };
            if (common.Action != "Index")
            {
                if (common.Action == "All")
                {
                    common.RevenueStatistics = await _cardService.RevenueStatisticsReponse();
                }
                if (common.Date != DateTime.MinValue && !string.IsNullOrEmpty(common.Action) && common.Action != "All")
                {
                    common.RevenueStatistics = await _cardService.GetRevenueStatisticsByActionAndDate(common.Action, common.Date);
                }
                else if (!string.IsNullOrEmpty(common.Action) && common.Date == DateTime.MinValue)
                {
                    common.RevenueStatistics = await _cardService.GetRevenueStatisticsByAction(common.Action);
                }
                else if (common.Date != DateTime.MinValue && (string.IsNullOrEmpty(common.Action) || common.Action != "All"))
                {
                    common.RevenueStatistics = await _cardService.GetRevenueStatisticsByDate(common.Date);
                }
            }
            var datas = common.RevenueStatistics;
            var webRoot = "wwwroot/";
            var tempPath = @"export/export_template.xlsx";
            int stt = 1;
            var doanhthu = 0;
            var tra = 0;

            int NUMBER_IGNORE_ROW = 3;
            FileInfo tempFile = new FileInfo(Path.Combine(webRoot, tempPath));
            if (!tempFile.Exists) throw new Exception("Không tìm thấy file template");

            var fileName = "export_card.xlsx";
            var exportFolder = Path.Combine(webRoot, "export");

            if (!Directory.Exists(exportFolder))
                Directory.CreateDirectory(exportFolder);

            var filePath = @"export/" + fileName;
            var fullPath = Path.Combine(webRoot, filePath);

            FileInfo file = new FileInfo(fullPath);

            if (file.Exists)
            {
                file.Delete();
                tempFile.CopyTo(fullPath);
            }
            else
                tempFile.CopyTo(fullPath);

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage(file))
            {
                ExcelWorksheet detailSheet = package.Workbook.Worksheets[0];

                if (datas.Count > 0)
                {
                    var row = NUMBER_IGNORE_ROW;
                    detailSheet.InsertRow(row + 2, datas.Count - 2, row + 1);
                    foreach (var item in datas)
                    {
                        if (item.action == "ReturnCard")
                        {
                            tra += item.amount;
                        }
                        else
                        {
                            doanhthu += item.amount;
                        }

                        //if (!item.IsVerify)
                        //{
                        //    detailSheet.Cells[row, 2, row, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //    detailSheet.Cells[row, 2, row, 7].Style.Fill.BackgroundColor.SetColor(Color.OrangeRed);
                        //}
                        detailSheet.Cells["A" + row].Value = stt++;
                        detailSheet.Cells["B" + row].Value = item.cardNumber.ToString() ?? "";
                        detailSheet.Cells["C" + row].Value = item.action == "TopUp" ? "Nạp tiền" : item.action == "CreateNewCard" ? "Tạo mới" : "Trả thẻ";
                        detailSheet.Cells["D" + row].Value = item.date.ToString("dd-MM-yyyy HH:mm:ss");
                        detailSheet.Cells["E" + row].Value = string.Format("{0:n0}", item.amount);

                        row++;
                    }

                    row++;

                    detailSheet.Cells["D" + row].Value = "Tổng doanh thu";
                    detailSheet.Cells["D" + row].Style.Font.Bold = true;
                    detailSheet.Cells["E" + row].Value = string.Format("{0:n0}", doanhthu);
                    detailSheet.Cells["E" + row++].Style.Font.Bold = true;

                    detailSheet.Cells["D" + row].Value = "Tổng tiền trả";
                    detailSheet.Cells["D" + row].Style.Font.Bold = true;
                    detailSheet.Cells["E" + row].Value = string.Format("{0:n0}", tra);
                    detailSheet.Cells["E" + row++].Style.Font.Bold = true;

                    detailSheet.Cells["D" + row].Value = "Tổng còn lại";
                    detailSheet.Cells["D" + row].Style.Font.Bold = true;
                    detailSheet.Cells["E" + row].Value = string.Format("{0:n0}", (doanhthu - tra)) + " VND";
                    detailSheet.Cells["E" + row].Style.Font.Bold = true;

                    detailSheet.Cells[detailSheet.Dimension.Address].AutoFitColumns();
                }
                package.Save();
            }

            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;
            var pathBase = _httpContextAccessor.HttpContext.Request.PathBase;

            var link = $"{pathBase}/{filePath}";
            return Json(link);
        }
    }
}
