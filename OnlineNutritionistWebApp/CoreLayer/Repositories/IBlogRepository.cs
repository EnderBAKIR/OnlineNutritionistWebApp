using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IBlogRepository: IGenericRepository<Blog> 
    {
        public Task<List<Blog>> GetBlogWithNutrition();
        public Task<List<Blog>> GetLastBlogAsync(int id);
    }
}
