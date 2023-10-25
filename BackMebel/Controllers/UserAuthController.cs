using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.UserDtos;
using BackMebel.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackMebel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        public UserAuthController(IUserAuthService _userAuthService)
        {
            this._userAuthService = _userAuthService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceRegisterResponse>> Register(UserRegisterForm registerForm)
        {
            var response = await _userAuthService.Register(registerForm);
            return Ok(response);
        }

        [HttpGet("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(string email, string password)
        {
            var response = await _userAuthService.Login(email, password);
            return Ok(response);
        }

        [HttpGet("LoginId")]
        public async Task<ActionResult<ServiceResponse<int>>> LoginId(string email, string password)
        {
            var response = await _userAuthService.LoginId(email, password);
            return Ok(response);
        }
    }
}
