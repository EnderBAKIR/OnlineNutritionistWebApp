using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.ViewComponents.Blog
{
    public class _LastBlogs: ViewComponent
    {
        private readonly IBlogService _blogService;

        public _LastBlogs(IBlogService blogService)
        {
            _blogService = blogService;
        }


        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int id)

        {
            var value = await _blogService.GetLastBlogAsync(id);
            return View(value);

        }

    }
}
