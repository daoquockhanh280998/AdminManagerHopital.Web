using SwaggerAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.Service.JwtManager
{
    public interface IJwtManager
    {
        public string GenerateToken(User user);
    }
}