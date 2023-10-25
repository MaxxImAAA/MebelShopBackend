using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.FeedbackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.IService
{
    public interface IFeedbackService
    {
        Task<ServiceResponse<bool>> AddFeedback(FeedbackFormDto formdto);

        Task<ServiceResponse<List<FeedbackDto>>> GetAllFeedbacks();
    }
}
