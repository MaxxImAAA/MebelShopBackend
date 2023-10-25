using BackMebel.Domain.Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Interfaces
{
    public interface IMessageInterface : IBaseInterface<Message>
    {
        Task<List<Message>> GetAllMessagesSenderReciver(int Iduser1, int Iduser2);
    }
}
