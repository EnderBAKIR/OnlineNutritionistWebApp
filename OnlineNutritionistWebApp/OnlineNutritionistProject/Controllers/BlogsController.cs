using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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


          if(User.Identity.IsAuthenticated)
            {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.AppuserId = user.Id;//for comments and likes insert
            
            bool doesLike = await _blogFeatureService.DoesGetLikeFilter(user.Id, id);//burada userın likeı var mı yok mu kontrol ediyoruz//here we check if the user has a like

            if (doesLike)
            {
                var featurefilter = await _blogFeatureService.GetLikeFilter(user.Id, id);//burada userın likeı var mı yok mu kontrol ediyoruz//here we check if the user has a like
                if (featurefilter != null)
                {
                    ViewBag.LikeId = featurefilter.Id;//burada userın likeı varsa onun id sini alıyoruz//here we get the id of the user's like if it exists
                    ViewBag.Featurefilter = featurefilter.AppUserId;//burada userın likeı varsa onun id sini alıyoruz//here we get the id of the user's like if it exists
                    ViewBag.Status = featurefilter.status;//burada userın likeı varsa onun statusunu alıyoruz//here we get the status of the user's like if it exists
                }
            }
            }


            var likeCount = await _blogFeatureService.GetLikeForAppUser(id);//burada likeları alıyoruz//here we get the likes
            ViewBag.LikeCount = likeCount.Where(x => x.status == true).Count();

            var blog = await _blogService.GetBlogAsync(id);//burada bloğu idsine göre detaylarını alıyoruz//here we get the details of the blog by its id
            ViewBag.BlogId = blog.Id;
            return View(blog);
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment , int blogId)
        {

            comment.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            comment.CommentStatus = true;
            await _commentService.AddAsync(comment);
            return RedirectToAction("BlogsDetails", "Blogs" , new { id = blogId });

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
            await _blogFeatureService.UpdateAsync(blogfeature);//burada likeın statusu true ise false , false ise true yapıyoruz//here we make the status of the like true if it is false, false if it is true

            return RedirectToAction(nameof(Index));
        }




    }
}
