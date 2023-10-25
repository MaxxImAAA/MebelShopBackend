using BackMebel.Domain.Models.MessageModels;
using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.MessageDtos;
using BackMebel.Service.Dtos.UserDtos;
using BackMebel.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackMebel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _message;
        public MessageController(IMessageService _message)
        {
            this._message = _message;
        }

        [HttpPost("SendMessage")]
        public async Task<ActionResult<ServiceResponse<bool>>> SendMessage(MessageSendDto messageSendDto)
        {
            var response = await _message.SendMessage(messageSendDto);
            return Ok(response);
        }

        [HttpGet("GetDialog")]
        public async Task<ActionResult<ServiceResponse<List<ServiceMessage>>>> GetAllMessage(int reciverId, int senderId)
        {
            var response = await _message.GetAllMessage(reciverId, senderId);
            return Ok(response);
        }

        [HttpGet("GetListContact")]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> GetListContact(int userid)
        {
            var response = await _message.GetListContact(userid);
            return Ok(response);
        }
    }
}
