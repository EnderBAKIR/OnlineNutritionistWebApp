using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using CoreLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class BlogFeatureService : Service<BlogFeature>, IBlogFeatureService
	{
		private readonly IBlogFeatureRepository _blogFeatureRepository;

		public BlogFeatureService(IGenericRepository<BlogFeature> repository, IUnitOfWork unitOfWork, IBlogFeatureRepository blogFeatureRepository ) : base(repository, unitOfWork)
		{
			_blogFeatureRepository = blogFeatureRepository;
		}

      

        public async Task<bool> DoesGetLikeFilter(int userId , int blogId)
        {
            return await _blogFeatureRepository.DoesGetLikeFilter(userId , blogId);
        }

        public async Task<BlogFeature> GetLikeFilter(int UserId, int blogId)
        {
            return await _blogFeatureRepository.GetLikeFilter(UserId , blogId);
        }

        public async Task<List<BlogFeature>> GetLikeForAppUser(int id)
		{
			return await _blogFeatureRepository.GetLikeForAppUser(id);
		}

        public async Task<List<BlogFeature>> GetLikeListByAppUserId(int id)
        {
            return await _blogFeatureRepository.GetLikeListByAppUserId(id);
        }
    }
}
