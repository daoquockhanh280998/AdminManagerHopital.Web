using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.AdminViewModel
{
    public class CardInformationViewModel
    {
        public string id { get; set; }
        public int cardNumber { get; set; }
        public int money { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime expiredDate { get; set; }
        public string patientId { get; set; }
        public int status { get; set; }
        public string createdBy { get; set; }
    }
}
