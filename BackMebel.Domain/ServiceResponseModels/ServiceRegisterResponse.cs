using BackMebel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Domain.ServiceResponseModels
{
    public class ServiceRegisterResponse
    {
        public string Description { get; set; }
        public List<string> Errors { get; set; }
        public StatusCode StatusCode { get; set; }
    }
}
