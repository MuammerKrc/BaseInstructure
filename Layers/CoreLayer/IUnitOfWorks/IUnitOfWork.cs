using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.IRepositories;

namespace CoreLayer.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
        Task SaveChangeAsync();
        void SaveChange();
    }
}
