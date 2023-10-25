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
    public class UserAuthRealization : IUserAuthInterface
    {
        private readonly ApplicationDbContext db;
        public UserAuthRealization(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Create(UserAuth model)
        {
            await db.UserAuths.AddAsync(model);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(UserAuth model)
        {
            db.UserAuths.Remove(model);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<UserAuth> Get(int id)
        {
            return await db.UserAuths.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserAuth>> GetAll()
        {
            return await db.UserAuths.ToListAsync();
        }

        public async Task<UserAuth> GetByEmail(string email)
        {
            return await db.UserAuths.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Email==email);
        }
    }
}
