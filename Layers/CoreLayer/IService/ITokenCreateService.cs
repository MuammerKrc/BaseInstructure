using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using CoreLayer.Models.IdentityModels;
using SharedLayer.Configurations;

namespace CoreLayer.IService
{
    public interface ITokenCreateService
    {
        Task<UserTokenDto> CreateTokenForUser(AppUser user);
        Task<ClientTokenDto> CreateTokenForClient(ClientConfig clientConfig);
    }
}
