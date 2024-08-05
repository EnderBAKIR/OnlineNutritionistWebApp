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
    public class AppuserRepository : GenericRepository<AppUser>, IAppuserRepository
    {
        public AppuserRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<List<AppUser>> GetNutritionists()
        {
            return await _dbContext.AppUsers.Include(x=>x.Consultancy).Where(u => u.AppNutriId != null).ToListAsync();
        }

        public async Task<AppUser> GetNutritionistById(int id)
        {
            return await _dbContext.AppUsers.Include(x=> x.Blogs).Where(u=>u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<AppUser>> GetNutritionWithService()
        {
            
            return await _dbContext.AppUsers.Include(c=> c.Blogs).ToListAsync();
        }
    }
}
