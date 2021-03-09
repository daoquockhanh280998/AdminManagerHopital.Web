using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.CardManager
{
    public class PatientCardRequest
    {
        public string patientId { get; set; }
        public int cardNumber { get; set; }
        public string CreatedBy { get; set; }
    }
}
