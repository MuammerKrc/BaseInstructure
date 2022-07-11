using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;

namespace CoreLayer.IService
{
    public interface ITokenCreateService
    {
        Task<UserTokenDto> CreateTokenForUser();
        Task<ClientTokenDto> CreateTokenForClient();
    }
}
