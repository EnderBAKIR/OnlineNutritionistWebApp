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

        public List<Comment> GetCommentWithBlogs(int id)
        {
            
            return _dbContext.Comments.Where(x => x.BlogID == id).Include(x => x.AppUser).ToList();
        }
    }
}
