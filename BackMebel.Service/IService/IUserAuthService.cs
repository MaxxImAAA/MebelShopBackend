using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.UserModels;
using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.IService
{
    public interface IUserAuthService 
    {
        Task<ServiceRegisterResponse> Register(UserRegisterForm registerForm);

        Task<ServiceResponse<string>> Login(string email, string password);

        Task<ServiceResponse<int>> LoginId(string email, string password);

    }
}
