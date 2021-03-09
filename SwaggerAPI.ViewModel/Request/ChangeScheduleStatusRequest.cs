using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Request
{
    public class ChangeScheduleStatusRequest
    {
        public string id { get; set; }
        public int status { get; set; }
    }
}
