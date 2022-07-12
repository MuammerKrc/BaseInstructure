using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using CoreLayer.IService;
using SharedLayer.Dtos;

namespace ServiceLayer.Services
{
    public class UserService:IUserService
    {
        public Task<Response<UserDto>> CreateUserAsync(CreateUserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserDto>> GetUserByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
