using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Realization
{
    public class UserRealization : IUserInterface
    {
        private readonly ApplicationDbContext db;
        public UserRealization(ApplicationDbContext db)
        {
            this.db = db;   
        }

        public Task<bool> Create(User model)

        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User model)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(int id)
        {
            return await db.Users.Include(x=>x.Cart).Include(x=>x.Orders).Include(x=>x.Feedbacks).Include(x=>x.UserAuth).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

       
    }
}
