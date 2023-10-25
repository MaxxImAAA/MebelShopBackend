using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.ProductDtos;
using BackMebel.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackMebel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService _orderService)
        {
            this._orderService = _orderService;
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddOrder(int userId)
        {
            var response = await _orderService.AddOrder(userId);
            return Ok(response);
        }

        [HttpGet("GetAllOrder")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAllOrder(int userId)
        {

            var response = await _orderService.GetAllOrder(userId);
            return Ok(response);
        }
    }
}
