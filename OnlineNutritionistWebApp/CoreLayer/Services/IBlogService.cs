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
        public Task<List<Blog>> GetBlogWithNutrition();
        public Task<List<Blog>> GetLastBlogAsync(int id);
        public Task<List<Blog>> GetBlogForNutrition(int id);// for List blog in Nutritionist Area  , Nutritionist areada listelenecek bloglar için
    }
}
