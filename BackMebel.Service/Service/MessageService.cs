using AutoMapper;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.MessageModels;
using BackMebel.Domain.Models.UserModels;
using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.MessageDtos;
using BackMebel.Service.Dtos.UserDtos;
using BackMebel.Service.IService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Service
{
    public class MessageService : IMessageService
    {
        private readonly IUserInterface _userDal;
        private readonly IMessageInterface _messageDal;
        private readonly IMapper mapper;
        public MessageService(IUserInterface _userDal, IMessageInterface _messageDal, IMapper mapper)
        {
            this._userDal = _userDal;
            this._messageDal = _messageDal;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<ServiceMessage>>> GetAllMessage(int reciverId, int senderId)
        {
            var service = new ServiceResponse<List<ServiceMessage>>();
            try
            {
                var allmessagesUser1 = await _messageDal.GetAllMessagesSenderReciver(reciverId, senderId);
                var allmessagesUser2 = await _messageDal.GetAllMessagesSenderReciver(senderId, reciverId);

                List<ServiceMessage> AllMessages = new List<ServiceMessage>();

                var newServiceMessageuser1 = new List<ServiceMessage>();
                var newServiceMessageuser2 = new List<ServiceMessage>();

                foreach (var item in allmessagesUser1)
                {
                    var servicemessageuser1 = new ServiceMessage()
                    {
                        Content = item.Content,
                        userId = item.ReciverId,
                        MessageDateTime = DateTime.ParseExact(item.SendDateTime, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),

                    };
                    newServiceMessageuser1.Add(servicemessageuser1);

                }

                foreach(var item in allmessagesUser2)
                {
                    var servicemessageuser2 = new ServiceMessage()
                    {
                        Content = item.Content,
                        userId = item.ReciverId,
                        MessageDateTime = DateTime.ParseExact(item.SendDateTime, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),

                    };
                    newServiceMessageuser2.Add(servicemessageuser2 );
                }

                AllMessages = newServiceMessageuser1.Concat(newServiceMessageuser2).OrderBy(x=>x.MessageDateTime).ToList();
                

                service.Data = AllMessages;
                service.Description = "Ваш диалог";
                service.StatusCode = Domain.Enums.StatusCode.OK;

                if (AllMessages.Count == 0)
                {
                    service.Description = "Начните диалог с этим пользователем!";
                }


            }
            catch(Exception ex)
            {

            }
            return service;
        }

        public async Task<ServiceResponse<List<UserDto>>> GetListContact(int userid)
        {
            var service = new ServiceResponse<List<UserDto>>();
            try
            {
                var us = await _userDal.GetAll();
                var users = us.Where(x => x.Id != userid).ToList();
                service.Data = mapper.Map<List<UserDto>>(users);
                service.Description = "Все пользователи";

            }
            catch(Exception ex)
            {
                service.Description = $"[GetListContact] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;

            }
            return service;
        }

        public async Task<ServiceResponse<bool>> SendMessage(MessageSendDto messageSendDto)
        {
            
            var service = new ServiceResponse<bool>();
            try
            {
                var sender = await _userDal.Get(messageSendDto.SenderId); 
                var reciver = await _userDal.Get(messageSendDto.ReciverId); 

                DateTime sendTime = DateTime.Now;
                var newMessage = new Message()
                {
                    Content = messageSendDto.Content,
                    Reciver = reciver,
                    Sender = sender,
                    SendDateTime = sendTime.ToString("dd.MM.yyyy HH:mm:ss"),
                };

                var result = await _messageDal.Create(newMessage);

                if(result == true)
                {
                    service.Description = "Сообщение отправлено";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }
                else
                {
                    service.Description = "Произошла ошибка при отправке сообщения";
                    service.StatusCode = Domain.Enums.StatusCode.BadRequest;
                }

            }
            catch(Exception ex)
            {
                service.Description = $"[SendMessage] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }
    }
}
