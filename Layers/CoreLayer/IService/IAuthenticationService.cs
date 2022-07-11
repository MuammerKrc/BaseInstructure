using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;

namespace CoreLayer.IService
{
    public interface IAuthenticationService
    {
        Task<UserTokenDto> UserAuthentication(UserLoginDto userLogin);
        Task<ClientTokenDto> ClientAuthentication(ClientLoginDto clientLogin);
    }
}
