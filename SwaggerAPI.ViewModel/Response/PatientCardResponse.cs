using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Response
{
    public class PatientCardResponse
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string CardId { get; set; }
        public int CardNumber { get; set; }
        public int Money { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int CardStatus { get; set; }
    }
}
