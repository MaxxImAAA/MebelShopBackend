using BackMebel.Domain.Models.ProductModels;
using BackMebel.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Domain.Models.CartModels
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductCount { get; set; }

        public List<CartProduct> CartProducts { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
      
    }
}
