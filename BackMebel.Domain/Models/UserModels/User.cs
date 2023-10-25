using BackMebel.Domain.Models.CartModels;
using BackMebel.Domain.Models.FeedBackModels;
using BackMebel.Domain.Models.MessageModels;
using BackMebel.Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Domain.Models.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserAuthId { get; set; }
        public UserAuth UserAuth { get; set; }

        public Cart Cart { get; set; }
        public List<Order> Orders { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<Message> Messages { get; set; }
    }
}
