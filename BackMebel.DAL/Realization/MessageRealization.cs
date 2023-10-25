using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.MessageModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Realization
{
    public class MessageRealization : IMessageInterface
    {
        private readonly ApplicationDbContext db;
        public MessageRealization(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Create(Message model)
        {
           await db.Messages.AddAsync(model);
           await db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Message model)
        {
            throw new NotImplementedException();
        }

        public Task<Message> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Message>> GetAllMessagesSenderReciver(int Iduser1, int Iduser2)
        {
            return await db.Messages.Where(x => x.SenderId == Iduser2 && x.ReciverId == Iduser1).ToListAsync();
        }
    }
}
