using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.CardManager
{
  public class CreateCard
    {
        public int Money { get; set; }
        public string patientId { get; set; }
        public string CreatedBy { get; set; }
    }
}
