using BackMebel.Domain.Models.CartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Interfaces
{
    public interface ICartProductInterface : IBaseInterface<CartProduct>
    {
        Task<CartProduct> GetCartProduct(int userId, int productId);

        Task<List<CartProduct>> GetCartProducts(int userId);

        Task<bool> DeleteAllCartProductByUser(int userId);

        
    }
}
