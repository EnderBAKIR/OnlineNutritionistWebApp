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
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<List<Blog>> GetAll()
        {
            return await _dbContext.Blogs.ToListAsync();
        }

        public async Task<Blog> GetBlogAsync()
        {
            return await _dbContext.Blogs.Include(X => X.AppUser).FirstOrDefaultAsync();
        }

        public async Task<List<Blog>> GetBlogForNutrition(int id)
        {
            return await _dbContext.Blogs.Include(x => x.AppUser).Where(x => x.AppUserId == id).ToListAsync();
        }

        public async Task<List<Blog>> GetBlogWithNutrition()
        {
            return await _dbContext.Blogs.Include(X => X.AppUser).ToListAsync();
        }

        public async Task<List<Blog>> GetLastBlogAsync(int id)
        {
           return await  _dbContext.Blogs.OrderByDescending(b => b.Id).Take(4).ToListAsync();
        }

        
    }
}
