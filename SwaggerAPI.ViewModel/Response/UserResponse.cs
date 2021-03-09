using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.ViewModel.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprove { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
