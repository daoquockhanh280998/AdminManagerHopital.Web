
using DemoAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerAPI.Data.Entities;
using SwaggerAPI.ViewModel.UserRequest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerAPI.Core.IService.UserService
{
    public interface IUserService
    {
        List<User> GetUsers();

        User FindUserById(string id);

        bool InsertUser(User rq);

        bool DeleteUser(string id);

        bool UpdateUser(string userId, User rq);

        User FindUserByUserName(LoginRequest login);

        bool AddListUser(List<User> users);

        Task<DemoResponse<string>> Import(IFormFile formFile);

        Task<FileStreamResult> Export();
    }
}