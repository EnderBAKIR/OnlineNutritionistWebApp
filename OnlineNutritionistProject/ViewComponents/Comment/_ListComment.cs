using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.ViewComponents.Comment
{
    public class _ListComment : ViewComponent
    {

        private readonly ICommentService _commentService;

        public _ListComment(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IViewComponentResult Invoke(int id)

        {
            var value = _commentService.GetCommentWithBlogs(id);
            return View(value);

        }

    }
}
