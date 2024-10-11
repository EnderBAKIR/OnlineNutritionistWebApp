using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public interface IBlogFeatureService: IService<BlogFeature>
	{
		public Task<List<BlogFeature>> GetLikeForAppUser(int id);
		public Task<List<BlogFeature>> GetLikeListByAppUserId(int id);
        public Task<bool> DoesGetLikeFilter(int userId, int blogId);
        public Task<BlogFeature> GetLikeFilter(int userId, int blogId);
    }
}
