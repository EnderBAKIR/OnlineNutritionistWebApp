using AutoMapper;
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
    public class BlogService : Service<Blog>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IGenericRepository<Blog> repository, IUnitOfWork unitOfWork, IBlogRepository blogRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<Blog> GetBlogAsync(int id)
        {
            return await _blogRepository.GetBlogAsync(id);
        }

        public async Task<List<Blog>> GetBlogForNutrition(int id)
        {
            return await _blogRepository.GetBlogForNutrition(id);
        }

        public async Task<List<Blog>> GetBlogWithNutrition()
        {
            return await _blogRepository.GetBlogWithNutrition();
        }

        public async Task<List<Blog>> GetLastBlogAsync(int id)
        {
         return await _blogRepository.GetLastBlogAsync(id);
        }
    }
}
