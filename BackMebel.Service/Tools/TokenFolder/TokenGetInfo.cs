using Azure.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Tools.TokenFolder
{
    public class TokenGetInfo : TokenGetInfoInterface
    {
        public int TokenGetUserId(string token)
        {
          int userId = 0;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenRead = tokenHandler.ReadToken(token) as JwtSecurityToken;
            userId = int.Parse(tokenRead.Claims.First(claim => claim.Type == "sub").Value);

            return userId;
        }
    }
}
