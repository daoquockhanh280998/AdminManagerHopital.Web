using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Response
{
    public class ChangeScheduleStatusResponse
    {
        public string id { get; set; }
        public string shiftId { get; set; }
        public string patientId { get; set; }
        public int order { get; set; }
        public int status { get; set; }
        public bool bhyt { get; set; }
    }
}
