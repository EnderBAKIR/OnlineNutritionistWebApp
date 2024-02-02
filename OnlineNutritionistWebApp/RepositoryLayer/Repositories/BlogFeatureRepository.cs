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
	internal class BlogFeatureRepository : GenericRepository<BlogFeature>, IBlogFeatureRepository
	{
		public BlogFeatureRepository(Context dbContext) : base(dbContext)
		{
		}

        public void ChangeToFalse(int id)
        {
            var values = _dbContext.GetBookss.Find(id);
            if (values != null)
            {
                values.status = false;
                _dbContext.SaveChanges();
            }
        }

        public async Task<BlogFeature> GetLikeFilter(int id)
        {
            return await _dbContext.BlogFeatures.Include(x => x.AppUser).Include(x => x.Blog).Where(x => x.AppUserId == id).FirstOrDefaultAsync();
        }

        public async Task<List<BlogFeature>> GetLikeForAppUser()
		{
			return await _dbContext.BlogFeatures.Include(x => x.AppUser).Include(x=> x.Blog).ToListAsync();
		}
	}
}
