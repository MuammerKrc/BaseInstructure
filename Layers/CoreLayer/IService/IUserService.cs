using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using SharedLayer.Dtos;

namespace CoreLayer.IService
{
    public interface IUserService
    {
        Task<Response<UserDto>> CreateUserAsync(CreateUserDto userDto);
        Task<Response<UserDto>> GetUserByNameAsync(string userName);
    }
}
