using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.FeedbackDtos;
using BackMebel.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackMebel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService _feedbackService)
        {
            this._feedbackService = _feedbackService;
        }

        [HttpPost("AddFeedback")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddFeedback(FeedbackFormDto formdto)
        {
            var response = await _feedbackService.AddFeedback(formdto);
            return Ok(response);
        }

        [HttpGet("GetAllFeedback")]
        public async Task<ActionResult<ServiceResponse<List<FeedbackDto>>>> GetAllFeedbacks()
        {
            var response = await _feedbackService.GetAllFeedbacks();
            return Ok(response);
        }

    }
}
