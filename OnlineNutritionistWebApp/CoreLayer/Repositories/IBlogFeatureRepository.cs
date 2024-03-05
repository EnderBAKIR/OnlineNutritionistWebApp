using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
	public interface IBlogFeatureRepository : IGenericRepository<BlogFeature>
	{
		public Task<List<BlogFeature>> GetLikeForAppUser(int id);
        public Task<bool> DoesGetLikeFilter(int userId , int blogId);
        public Task<BlogFeature> GetLikeFilter(int userId , int blogId);
        
    }
}
