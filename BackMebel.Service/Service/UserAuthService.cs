using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.CartModels;
using BackMebel.Domain.Models.MessageModels;
using BackMebel.Domain.Models.OrderModels;
using BackMebel.Domain.Models.UserModels;
using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.UserDtos;
using BackMebel.Service.IService;
using BackMebel.Service.Tools.FluentValidation;
using BackMebel.Service.Tools.TokenFolder;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Service
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserAuthInterface _userAuthInterface;
        private readonly ICartInterface _cartInterface;
        private readonly IValidator<UserRegisterForm> _validator;
        private readonly ITokenInterface _token;
        public UserAuthService(IUserAuthInterface _userAuthInterface, ITokenInterface _token, ICartInterface _cartInterface, IValidator<UserRegisterForm> _validator)
        {   
            this._userAuthInterface = _userAuthInterface;
            this._cartInterface = _cartInterface;
            this._validator = _validator;
            this._token = _token;
        }

        public  async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var service = new ServiceResponse<string>();
            try
            {
                var userauth = await _userAuthInterface.GetByEmail(email);
                if(userauth == null)
                {
                    service.Description = "ПОльзователь не найден";
                    service.StatusCode = Domain.Enums.StatusCode.NotFound;
                }
                else
                {
                    if(!BCrypt.Net.BCrypt.Verify(password, userauth.Password))
                    {
                        service.Description = "Неверный пароль";
                        service.StatusCode = Domain.Enums.StatusCode.BadRequest;
                    }
                    else
                    {
                        var token = _token.GetToken(userauth.User.Id, userauth.User.Name);
                        service.Data = token;
                        service.Description = "Вы успешно вошли";
                        service.StatusCode = Domain.Enums.StatusCode.OK;
                    }
                }

            }
            catch(Exception ex)
            {
                service.Description = $"[Login] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<int>> LoginId(string email, string password)
        {
            var service = new ServiceResponse<int>();
            try
            {
                var userauth = await _userAuthInterface.GetByEmail(email);
                if (userauth == null)
                {
                    service.Description = "Пользователь не найден";
                    service.StatusCode = Domain.Enums.StatusCode.NotFound;
                }
                else
                {
                    if (!BCrypt.Net.BCrypt.Verify(password, userauth.Password))
                    {
                        service.Description = "Неверный пароль";
                        service.StatusCode = Domain.Enums.StatusCode.BadRequest;
                    }
                    else
                    {
                        //var token = _token.GetToken(userauth.User.Id, userauth.User.Name);
                        // service.Data = token;
                        service.Data = userauth.User.Id;
                        service.Description = "Вы успешно вошли";
                        service.StatusCode = Domain.Enums.StatusCode.OK;
                    }
                }

            }
            catch(Exception ex)
            {
                service.Description = $"[LoginId] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceRegisterResponse> Register(UserRegisterForm registerForm)
        {
            var validationResult = _validator.Validate(registerForm);
            var service = new ServiceRegisterResponse();
           
            try 
            {
                if(validationResult.IsValid) 
                {
                    var password = BCrypt.Net.BCrypt.HashPassword(registerForm.Password);

                    UserAuth newUserAuth = new UserAuth()
                    {
                        Email = registerForm.Email,
                        Password = password,

                    };
                    User newUser = new User()
                    {
                        Name = registerForm.Name,
                        UserAuth = newUserAuth,

                        Cart = new Cart()
                        {
                            ProductCount = 0,

                        },
                        Orders = new List<Order>(),
                        Messages = new List<Message>(),
                        
                        
                    };
                    newUserAuth.User = newUser;

                    var result = await _userAuthInterface.Create(newUserAuth);
                    if (result == true)
                    {
                        service.Description = "Регистрация прошла успешно";
                        service.StatusCode = Domain.Enums.StatusCode.OK;
                    }
                    else
                    {
                        service.Description = "Произошла ошибка регистрации";
                        service.StatusCode = Domain.Enums.StatusCode.BadRequest;
                    }

                }
                if(!validationResult.IsValid)
                {
                    service.Errors = new List<string>();
                    var errors = validationResult.Errors.Select(e => new
                    {
                        Field = e.PropertyName,  // Имя поля
                        Message = e.ErrorMessage  // Сообщение об ошибке
                    });

                    foreach(var item in errors)
                    {
                        service.Errors.Add(item.Message);
                    }
                    service.Description = "Неверно введенные данные";
                    service.StatusCode = Domain.Enums.StatusCode.BadRequest;

                }
               


            }
            catch(Exception ex)
            {
                service.Description = $"[Register] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;

        }
    }
}
