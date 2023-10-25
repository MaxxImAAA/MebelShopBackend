using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.CartModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Realization
{
    public class CartRealization : ICartInterface 
    {
        private readonly ApplicationDbContext db;
        public CartRealization(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(Cart model)
        {
            await db.Carts.AddAsync(model);
            await db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Cart model)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cart>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartByUser(int userId)
        {
            return await db.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
