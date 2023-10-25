using AutoMapper;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.FeedBackModels;
using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.FeedbackDtos;
using BackMebel.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUserInterface _userDal;
        private readonly IFeedbackInterface _feedbackDal;
        private readonly IMapper mapper;
        public FeedbackService(IUserInterface _userDal, IFeedbackInterface _feedbackDal, IMapper mapper)
        {
            this._userDal = _userDal;
            this._feedbackDal = _feedbackDal;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> AddFeedback(FeedbackFormDto formdto)
        {
           var service = new ServiceResponse<bool>();
            try
            {
                var user = await _userDal.Get(formdto.userid);


                var newFeedBack = new Feedback()
                {
                    Message = formdto.message,
                    Email = user.UserAuth.Email,
                    Name = user.Name,
                    User = user,
                };
                var result = await _feedbackDal.Create(newFeedBack);
                if(result == true)
                {
                    service.Description = "Отзыв добавлен";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }
                else
                {
                    service.Description = "Не удалосб доабавить отзыв";
                    service.StatusCode = Domain.Enums.StatusCode.BadRequest;
                }


            }
            catch (Exception ex)
            {
                service.Description = $"[AddFeedback] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<List<FeedbackDto>>> GetAllFeedbacks()
        {
            var service = new ServiceResponse<List<FeedbackDto>>();
            try
            {
                var feedbacks = await  _feedbackDal.GetAll();
                if(feedbacks == null)
                {
                    service.Description = "Отзывов нет";
                    service.StatusCode = Domain.Enums.StatusCode.NotFound;
                }
                if(feedbacks != null)
                {
                    service.Data = mapper.Map<List<FeedbackDto>>(feedbacks);
                    service.Description = "Выведены все отзывы";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }

            }
            catch(Exception ex)
            {
                service.Description = $"[GetAllFeedbacks] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }
    }
}
