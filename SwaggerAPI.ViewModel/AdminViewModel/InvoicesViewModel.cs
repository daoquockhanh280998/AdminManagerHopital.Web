using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.AdminViewModel
{
    public class InvoicesViewModel
    {
        public string id { get; set; }
        public DateTime createdDate { get; set; }
        public string scheduleId { get; set; }
        public string patientId { get; set; }
        public string content { get; set; }
        public int cost { get; set; }
        public string createBy { get; set; }

        public int status { get; set; }
    }
}
