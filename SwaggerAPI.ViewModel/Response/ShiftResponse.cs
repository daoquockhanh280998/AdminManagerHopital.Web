using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Response
{
    public class ShiftResponse
    {
        public string Id { get; set; }

        public string RoomId { get; set; }

        public string DoctorId { get; set; }

        public DateTime Date { get; set; }

        public string TimeId { get; set; }
    }
}
