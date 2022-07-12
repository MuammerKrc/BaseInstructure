using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.BaseModels;

namespace CoreLayer.Models.JwtModels
{
    public class UserRefreshToken:BaseModel<long>
    {
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public long UserId { get; set; }
    }
}
