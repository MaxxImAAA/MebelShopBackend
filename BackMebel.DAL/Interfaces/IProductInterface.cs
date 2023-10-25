using BackMebel.Domain.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Interfaces
{
    public interface IProductInterface : IBaseInterface<Product>
    {
        Task<List<Product>> GetAllProductsByCartProducts(int userId);

        Task<List<Product>> GetAllProductByOrderProduct(int userId);

        Task<List<Product>> GetAllProductByType(string type);

        Task<List<Product>> GetAllProductByName(string name);
    }
}
