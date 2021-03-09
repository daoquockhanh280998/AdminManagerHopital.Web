using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.CalendarRequest
{
    public class CalendarRequest
    {
        public string RoomId { get; set; }
        public string DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string TimeId { get; set; }
    }
}
