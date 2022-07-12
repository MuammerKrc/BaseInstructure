using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.IUnitOfWorks;

namespace DataLayer.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task SaveChangeAsync()
        {
            await context.SaveChangesAsync();
        }

        public void SaveChange()
        {
            context.SaveChanges();
        }
    }
}
