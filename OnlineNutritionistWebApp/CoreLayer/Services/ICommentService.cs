using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface ICommentService: IService<Comment>
    {
        public List<Comment> GetCommentWithBlogs(int id);
        public Task<List<Comment>> GetCommentWithBlogList(int id); //Member Area'da üyenin yorumunu listeleme işlemi. //The process of listing the member's comment in the member area.
    }
}
