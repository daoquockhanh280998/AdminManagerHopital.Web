using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.CardManager
{
    public class CardReponse
    {
        public string id { get; set; }
        public int cardNumber { get; set; }
        public int money { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime expiredDate { get; set; }
        public string patientId { get; set; }
        public string status { get; set; }
    }
}
