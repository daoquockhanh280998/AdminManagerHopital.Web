using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Response
{
    public class ActionResponse
    {
        public int cardNumber { get; set; }
        public string action { get; set; }
        public int amount { get; set; }
        public DateTime date { get; set; }
    }
}
