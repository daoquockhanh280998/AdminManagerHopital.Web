using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SwaggerAPI.ViewModel.Request
{
   public class LoginRequest
    {
        [Required(ErrorMessage ="Vui Lòng Nhập Tài Khoản")]
        public string username { get; set; }

        [Required(ErrorMessage = "Vui Lòng Nhập Mật Khẩu")]
        public string password { get; set; }
    }
}
