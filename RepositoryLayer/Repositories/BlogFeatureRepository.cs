using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
	public class BlogFeatureRepository : GenericRepository<BlogFeature>, IBlogFeatureRepository
	{
		public BlogFeatureRepository(Context dbContext) : base(dbContext)
		{
		}

       

        public async Task<bool> DoesGetLikeFilter(int userId , int blogId)
        {
            return await _dbContext.BlogFeatures.Include(x => x.AppUser).Include(x=>x.Blog).AnyAsync(x => x.AppUserId == userId && x.BlogId == blogId);//burada kullanıcının blogu beğenip beğenmediğini kontrol ediyoruz//here we check if the user likes the blog
        }

        public async Task<BlogFeature> GetLikeFilter(int userId , int blogId)//burada kullanıcının beğenilerini alıyoruz//here we get the likes of the user
        {
            return await _dbContext.BlogFeatures
            .Include(x => x.AppUser)
            .Include(x => x.Blog)
            .Where(x => x.AppUserId==userId && x.BlogId==blogId)
            .FirstOrDefaultAsync();
        }

        public async Task<List<BlogFeature>> GetLikeForAppUser(int id)
		{
			return await _dbContext.BlogFeatures.Include(x => x.AppUser).Include(x=> x.Blog).Where(x=>x.BlogId==id).ToListAsync();//burada blogun id sine göre likeları alıyoruz//here we get the likes by the id of the blog
		}

        public async Task<List<BlogFeature>> GetLikeListByAppUserId(int id)
        {
            return await _dbContext.BlogFeatures.Include(x => x.AppUser).Include(x => x.Blog).Where(x => x.AppUserId == id).ToListAsync();//burada blogun id sine göre likeları alıyoruz//here we get the likes by the id of the blog
        }
    }
}
