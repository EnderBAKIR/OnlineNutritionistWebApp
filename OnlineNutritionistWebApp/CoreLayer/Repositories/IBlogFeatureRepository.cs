using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
	public interface IBlogFeatureRepository : IGenericRepository<BlogFeature>
	{
		public Task<List<BlogFeature>> GetLikeForAppUser();
        public Task<BlogFeature> GetLikeFilter(int id);
        void ChangeToFalse(int id);
    }
}
