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

        public Blog GetBlogWithNutrition(int id)
        {
            return _dbContext.Blogs.Where(x=> x.Id == id).Include(X=> X.AppUser).FirstOrDefault();
        }
    }
}
