﻿using CoreLayer.Models;
using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(Context dbContext) : base(dbContext)
        {
        }


        public async Task<Blog> GetBlogAsync(int id)//hergangi bir kullanıcının blogları görmesi için
        {
            return await _dbContext.Blogs.Include(x => x.AppUser).Include(x => x.Comments).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Blog>> GetBlogForNutrition(int id)//Nutrition areada blogların giriş yapan diyetisyene göre listelenmesi için
        {
            return await _dbContext.Blogs.Include(x => x.AppUser).Where(x => x.AppUserId == id).ToListAsync();
        }

        public async Task<List<Blog>> GetBlogWithNutrition()//blogları indexde listelemek için
        {
            return await _dbContext.Blogs.Include(X => X.AppUser).Include(x => x.BlogFeature).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<Blog> GetDetailsBlogAsync(int id)
        {
            return await _dbContext.Blogs.Include(x => x.AppUser).Include(x => x.Comments).ThenInclude(c => c.AppUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Blog>> GetLastBlogAsync(int id)//son eklenen 4 blogun listelenmesi için
        {
            return await _dbContext.Blogs.OrderByDescending(b => b.Id).Take(4).ToListAsync();
        }

        public async Task<List<Blog>> GetPopularBlogsAsync()
        {
            var twoDaysAgo = DateTime.Now.AddDays(-2);
            return await _dbContext.Blogs
                .Include(user => user.AppUser)
                .Include(like => like.BlogFeature)
                .OrderByDescending(x => x.BlogFeature
                .Count(likeDate => likeDate.LikeDate >= twoDaysAgo))
                .Take(4).ToListAsync();
        }
    }
}
