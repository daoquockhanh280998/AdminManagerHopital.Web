using SwaggerAPI.ViewModel.CalendarRequest;
using SwaggerAPI.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.AdminViewModel
{
    public class AdminViewModel
    {
        public string Room { get; set; }

        public int Status { get; set; }

     
        public List<PatientEaminationListRequest> listRequests { get; set; }

        public List<RoomRequestModel> listRooms { get; set; }
    }
}
