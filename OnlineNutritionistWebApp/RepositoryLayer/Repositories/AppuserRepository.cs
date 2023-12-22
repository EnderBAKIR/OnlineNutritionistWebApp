using CoreLayer.Models;
using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class AppuserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppuserRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await _dbContext.AppUsers.ToListAsync();
        }
    }
}
