using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IBlogService : IService<Blog>
    {
        public Blog GetBlogWithNutrition(int id);
        public Task<List<Blog>> GetLastBlogAsync(int id);
    }
}
