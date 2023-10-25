using BackMebel.Domain.Models.CartModels;
using BackMebel.Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Domain.Models.ProductModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public string ProductType { get; set; }
        public int Price { get;set; }

       
        public List<OrderProduct> OrderProducts { get; set; }
        public List<CartProduct> CartProducts { get; set; }
    }
}
