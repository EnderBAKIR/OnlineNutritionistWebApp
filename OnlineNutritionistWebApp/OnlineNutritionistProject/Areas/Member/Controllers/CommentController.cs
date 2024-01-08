using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Sources;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [AllowAnonymous]
    [Area("Member")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userService;

        public CommentController(ICommentService commentService, UserManager<AppUser> user)
        {
            _commentService = commentService;
            _userService = user;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userService.FindByNameAsync(User.Identity.Name);
            var comment = await _commentService.GetCommentWithBlogList(values.Id);
            return View(comment);
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var values = await _commentService.GetByIdAsync(id);
            await _commentService.RemoveAsync(values);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> EditComment(int id)
        {
            var values = await _commentService.GetByIdAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(Comment comment)
        {
            await _commentService.UpdateAsync(comment);
            return RedirectToAction(nameof(Index));
        }
    }
}
