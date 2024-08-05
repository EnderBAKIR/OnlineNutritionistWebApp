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
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork, ICommentRepository commentRepository) : base(repository, unitOfWork)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<Comment>> GetCommentWithBlogList(int id)
        {
            return await _commentRepository.GetCommentWithBlogList(id);
        }

        public List<Comment> GetCommentWithBlogs(int id)
        {
            return _commentRepository.GetCommentWithBlogs(id);
        }
    }
}
