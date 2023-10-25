using BackMebel.Domain.Models.CartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Interfaces
{
    public interface ICartInterface : IBaseInterface<Cart>
    {

        Task<Cart> GetCartByUser(int userId);

    }
}
