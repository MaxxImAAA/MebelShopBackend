using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Domain.Models.MessageModels
{
    public class ServiceMessage
    {
        public int userId { get; set; }
        public DateTime MessageDateTime { get; set; }
        public string Content { get; set; }
    }
}
