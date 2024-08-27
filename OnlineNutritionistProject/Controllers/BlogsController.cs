using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ServiceLayer.Services;
using System.Reflection.Metadata;

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

        public async Task<IActionResult> Index()//Blogların listelendiği sayfa//List of blogs page  
        {
            return View(await _blogService.GetBlogWithNutrition());
        }


        [HttpGet]
        public async Task<IActionResult> BlogsDetails(int id)
        {
            TempData["blogId"] = id;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.AppuserId = user.Id;//For comments and likes insert.

                bool doesLike = await _blogFeatureService.DoesGetLikeFilter(user.Id, id);//Kullanıcının beğenisi var mı kontrol edilir.//Checking if the user has likes.

                if (doesLike)
                {
                    var featurefilter = await _blogFeatureService.GetLikeFilter(user.Id, id);//Kullanıcının beğenisi var mı yok mu kontrol edilir.//Checking whether the user has likes or not.
                    if (featurefilter != null)
                    {
                        ViewBag.LikeId = featurefilter.Id;//Kullanıcının beğenisi varsa onun id si alınır.//If the user has a like, his ID is taken.
                        ViewBag.Featurefilter = featurefilter.AppUserId;//Kullanıcının beğenisi varsa onun id si alınır.//If the user has a like, we get her ID.
                        ViewBag.Status = featurefilter.status;//Kullanıcının beğenisi varsa onun statusunu alınır.//If the user has likes, we get their status.
                    }
                }
            }

            var likeCount = await _blogFeatureService.GetLikeForAppUser(id);//Beğeniler alınır.//Likes are received.
            ViewBag.LikeCount = likeCount.Where(x => x.status == true).Count();

            var blog = await _blogService.GetBlogAsync(id);//Bloğu idsine göre detayları alınır.//Details are taken according to the id of the block.
            var blogdetail = await _blogService.GetDetailsBlogAsync(blog.Id);
            ViewBag.BlogId = blog.Id;
            return View(blog);
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment, int blogId)
        {

            comment.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            comment.CommentStatus = true;
            await _commentService.AddAsync(comment);
            return RedirectToAction("BlogsDetails", "Blogs", new { id = blogId });

        }

        [HttpGet]
        public IActionResult LikeCount()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LikeCount(BlogFeature blogfeature)
        {
            blogfeature.status = true;
            blogfeature.LikeDate = DateTime.Now;
            await _blogfeature.AddAsync(blogfeature);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> LikeBack(BlogFeature blogfeature)
        {
            await _blogFeatureService.UpdateAsync(blogfeature);//likeın statusu true ise false , false ise true yapılır.//If the status of the like is true, it is set to false, otherwise it is set to true.

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> PinComment(Comment comment, int id)
        {
            comment = await _commentService.GetByIdAsync(id);

            comment.CommentStatus = true;
            comment.PinnetComment = true;

            await _commentService.UpdateAsync(comment);

            return RedirectToAction(nameof(BlogsDetails), new { id = TempData["blogId"] });
        }

        public async Task<IActionResult> UnpinComment(Comment comment, int id)
        {
            comment = await _commentService.GetByIdAsync(id);

            comment.CommentStatus = true;
            comment.PinnetComment = false;

            await _commentService.UpdateAsync(comment);

            return RedirectToAction(nameof(BlogsDetails), new { id = TempData["blogId"] });
        }


    }
}
