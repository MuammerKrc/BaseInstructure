using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.IRepositories;
using CoreLayer.Models.JwtModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class UserRefreshTokenRepository:BaseRepository<UserRefreshToken,long>,IUserRefreshTokenRepository
    {
        
        public UserRefreshTokenRepository(DbContext context) : base(context)
        {

        }
    }
}
