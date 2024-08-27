using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<List<Blog>> GetBlogWithNutrition();
        Task<List<Blog>> GetLastBlogAsync(int id);
        Task<List<Blog>> GetBlogForNutrition(int id);// for List blog in Nutritionist Area  , Nutritionist areada listelenecek bloglar için
        Task<Blog> GetBlogAsync(int id);
        Task<Blog> GetDetailsBlogAsync(int id);

    }
}
