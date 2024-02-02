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
		public Task<List<BlogFeature>> GetLikeForAppUser();
        public Task<BlogFeature> GetLikeFilter(int id);
        void ChangeToFalse(int id);
    }
}
