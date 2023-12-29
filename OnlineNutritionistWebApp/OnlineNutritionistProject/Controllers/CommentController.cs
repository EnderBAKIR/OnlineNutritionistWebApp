using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Newtonsoft.Json.Linq;

namespace OnlineNutritionistProject.Controllers
{
    public class CommentController : Controller
    {

        private readonly IService<Comment> _commentService;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(IService<Comment> commentService)
        {
            _commentService = commentService;
        }


        public IActionResult _CommentPartial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> _CommentPartial(Comment comment)
        {
            ViewBag.Commentcontent = comment.CommentContent;
            comment.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            comment.CommentStatus = true;
            await _commentService.AddAsync(comment);
            return RedirectToAction("Index", "Blogs");


        }
    }
}
