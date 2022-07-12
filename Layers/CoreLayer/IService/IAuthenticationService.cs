using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using SharedLayer.Dtos;

namespace CoreLayer.IService
{
    public interface IAuthenticationService
    {
        Task<Response<UserTokenDto>> UserAuthentication(UserLoginDto userLogin);
        Task<Response<ClientTokenDto>> ClientAuthentication(ClientLoginDto clientLogin);
    }
}
