using CoreLayer.Models;
using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<List<Comment>> GetCommentWithBlogList(int id)
        {
            return await _dbContext.Comments.Include(x => x.Blog).Where(x => x.AppUserId == id).ToListAsync();
        }

        public List<Comment> GetCommentWithBlogs(int id)
        {
            
            return _dbContext.Comments.Where(x => x.BlogId == id).Include(x => x.AppUser).ToList();
        }
    }
}
