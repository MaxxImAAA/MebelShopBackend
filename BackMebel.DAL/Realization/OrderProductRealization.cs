using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Realization
{
    public class OrderProductRealization : IOrderProductInterface
    {
        private readonly ApplicationDbContext db;
        public OrderProductRealization(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Create(OrderProduct model)
        {
          await db.OrderProducts.AddAsync(model);
           await db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(OrderProduct model)
        {
            throw new NotImplementedException();
        }

        public Task<OrderProduct> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderProduct>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
