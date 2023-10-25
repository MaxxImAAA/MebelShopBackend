using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Dtos.FeedbackDtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
