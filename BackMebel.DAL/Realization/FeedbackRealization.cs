using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.FeedBackModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Realization
{
    public class FeedbackRealization : IFeedbackInterface
    {
        private readonly ApplicationDbContext db;
        public FeedbackRealization(ApplicationDbContext db)
        {
            this.db = db;
            
        }
        public async Task<bool> Create(Feedback model)
        {
           await db.Feedbacks.AddAsync(model);
           await db.SaveChangesAsync();
            return true;
            
        }

        public Task<bool> Delete(Feedback model)
        {
            throw new NotImplementedException();
        }

        public Task<Feedback> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Feedback>> GetAll()
        {
            return await db.Feedbacks.ToListAsync();
        }
    }
}
