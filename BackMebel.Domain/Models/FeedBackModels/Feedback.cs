using BackMebel.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Domain.Models.FeedBackModels
{
    public class Feedback
    {
        public int Id { get; set; }

        public string Message { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
