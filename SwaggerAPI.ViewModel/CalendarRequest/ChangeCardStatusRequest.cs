using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.CalendarRequest
{
    public class ChangeCardStatusRequest
    {
        public int cardNumber { get; set; }
        public string status { get; set; }
        public string CreatedBy { get; set; }
    }
}
