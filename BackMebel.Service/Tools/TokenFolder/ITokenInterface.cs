using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Tools.TokenFolder
{
    public interface ITokenInterface
    {
        string GetToken(int id, string name);
    }
}
