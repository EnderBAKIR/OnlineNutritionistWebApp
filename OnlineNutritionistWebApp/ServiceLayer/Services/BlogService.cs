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


        public Blog GetBlogWithNutrition(int id)
        {
            return _blogRepository.GetBlogWithNutrition(id);
        }
    }
}
