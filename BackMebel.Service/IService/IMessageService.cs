using BackMebel.Domain.Models.MessageModels;
using BackMebel.Domain.Models.UserModels;
using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.MessageDtos;
using BackMebel.Service.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.IService
{
    public  interface IMessageService
    {
        Task<ServiceResponse<bool>> SendMessage(MessageSendDto messageSendDto);
        Task<ServiceResponse<List<ServiceMessage>>> GetAllMessage(int reciverId, int senderId);

        Task<ServiceResponse<List<UserDto>>> GetListContact(int userid);
    }
}
