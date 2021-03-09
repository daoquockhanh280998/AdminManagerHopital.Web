using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Request
{
    public class RegistrationInformationRequest
    {
        public string Name { get; set; }
      
        public DateTime Birthday { get; set; }
       
        public bool Gender { get; set; }
        
        public string Phone { get; set; }
      
        public string Address { get; set; }

     //   public int PatientId { get; set; }

    }
}
