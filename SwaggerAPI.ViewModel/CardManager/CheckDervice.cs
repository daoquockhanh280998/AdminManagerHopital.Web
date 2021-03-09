using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.CardManager
{
    public class CheckDervice
    {
        public string NameService { get; set; }
        public string patientId { get; set; }
        public int cardNumber { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string Adress { get; set; }
        public int Money { get; set; }
    }
}
