using BackMebel.Domain.Models.ProductModels;
using BackMebel.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Domain.Models.OrderModels
{
    public class Order
    {
        public int Id { get; set; }
        public int CountProduct { get; set; }

        public DateTime CreatedDate { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
