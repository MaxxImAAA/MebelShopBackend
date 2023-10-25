using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Dtos.MessageDtos
{
    public class MessageSendDto
    {
        public string Content { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get;set; }
    }
}
