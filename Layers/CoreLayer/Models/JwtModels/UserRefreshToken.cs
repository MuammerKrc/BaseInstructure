using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models.JwtModels
{
    public class UserRefreshToken
    {
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
