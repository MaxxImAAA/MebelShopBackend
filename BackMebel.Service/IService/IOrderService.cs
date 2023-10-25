using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.IService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> AddOrder(int userId);

        Task<ServiceResponse<List<ProductDto>>> GetAllOrder(int userId);
    }
}
