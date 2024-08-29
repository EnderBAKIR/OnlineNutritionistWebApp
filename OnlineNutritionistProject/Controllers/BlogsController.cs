using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class BlogsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IService<BlogFeature> _blogfeature;
        private readonly IBlogFeatureService _blogFeatureService;

        public BlogsController(ICommentService commentService, UserManager<AppUser> userManager, IBlogService blogService, IService<BlogFeature> blogfeature, IBlogFeatureService blogFeatureService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _blogService = blogService;
            _blogfeature = blogfeature;
            _blogFeatureService = blogFeatureService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetBlogWithNutrition());
        }

        [HttpGet]
        public async Task<IActionResult> BlogsDetails(int id)
        {
            TempData["blogId"] = id;

            var blog = await _blogService.GetBlogAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            var likeCount = await _blogFeatureService.GetLikeForAppUser(id);
            ViewBag.LikeCount = likeCount.Count(x => x.status);

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.AppUserId = user.Id;

                var userLike = await _blogFeatureService.GetLikeFilter(user.Id, id);
                if (userLike != null)
                {
                    ViewBag.LikeId = userLike.Id;
                    ViewBag.UserLikeStatus = userLike.status;
                }
            }

            return View(blog);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCommentAjax(Comment comment)
        {
            comment.CreatedDate = DateTime.Now;
            comment.CommentStatus = true;
            await _commentService.AddAsync(comment);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return Json(new
            {
                success = true,
                commentId = comment.Id,
                userName = user?.Name + " " + user?.Surname,
                userImage = user?.ImageUrl, 
                commentDate = comment.CreatedDate.ToString("dd MMM yyyy"),
                commentContent = comment.CommentContent
            });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ToggleLikeAjax(int blogId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            var existingLike = await _blogFeatureService.GetLikeFilter(user.Id, blogId);
            if (existingLike != null)
            {
                existingLike.status = !existingLike.status;
                await _blogFeatureService.UpdateAsync(existingLike);
            }
            else
            {
                var newLike = new BlogFeature
                {
                    BlogId = blogId,
                    AppUserId = user.Id,
                    status = true,
                    LikeDate = DateTime.Now
                };
                await _blogfeature.AddAsync(newLike);
            }

            var likeCount = await _blogFeatureService.GetLikeForAppUser(blogId);
            return Json(new { success = true, likeCount = likeCount.Count(x => x.status) });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PinCommentAjax(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return Json(new { success = false, message = "Yorum bulunamadı." });

            comment.PinnetComment = !comment.PinnetComment;
            await _commentService.UpdateAsync(comment);

            return Json(new { success = true, isPinned = comment.PinnetComment });
        }
    }
}