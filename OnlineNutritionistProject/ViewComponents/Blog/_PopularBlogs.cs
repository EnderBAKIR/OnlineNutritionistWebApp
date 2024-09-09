using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.ViewComponents.Blog
{
    public class _PopularBlogs : ViewComponent
    {
        private readonly IBlogService _blogService;

        public _PopularBlogs(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _blogService.GetPopularBlogsAsync();
            return View(blogs);

        }
    }
}
