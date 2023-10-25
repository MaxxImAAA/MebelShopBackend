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
    public class OrderRealization : IOrderInterface
    {
        private readonly ApplicationDbContext db;
        public OrderRealization(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Create(Order model)
        {
            await db.Orders.AddAsync(model);
            await db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Order model)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
