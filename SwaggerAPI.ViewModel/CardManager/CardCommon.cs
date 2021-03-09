using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.CardManager
{
  public class CardCommon
    {
        public string Action { get; set; }
        public DateTime Date { get; set; }
        public List<RevenueStatisticsReponse> RevenueStatistics { get; set; }
    }
}
