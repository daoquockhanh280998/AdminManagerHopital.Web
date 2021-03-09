using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Request
{
    public class ScheduleRequest
    {
        public string name { get; set; }
        public DateTime birthday { get; set; }
        public bool gender { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string patientId { get; set; }
        public string shiftId { get; set; }
        public bool bhyt { get; set; }
    }
}
