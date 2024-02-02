using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class BlogsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IService<BlogFeature> _blogfeature;
        private readonly IBlogFeatureService _blogFeatureService;
        public BlogsController(UserManager<AppUser> userManager, IBlogService blogService, IService<BlogFeature> blogfeature, IBlogFeatureService blogFeatureService)
        {
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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.AppuserId = user.Id;//for comments and likes insert


            ViewBag.BlogId = id;
            var blog = await _blogService.GetBlogAsync();
            var featurefilter =await _blogFeatureService.GetLikeFilter(user.Id);
            if (featurefilter != null)
            {
             ViewBag.LikeId = featurefilter.Id;
             ViewBag.Featurefilter = featurefilter.AppUserId;
             ViewBag.Status = featurefilter.status;
            }
           

            
            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> LikeCount(int id)
        {
            var blog = await _blogService.GetBlogAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.blog = blog.Id;
            ViewBag.user = user.Id;

            var value = _blogfeature.GetByIdAsync(id);
            return View(value);
        }



        [HttpPost]
        public async Task<IActionResult> LikeCount(BlogFeature blogfeature)
        {
            var blog = await _blogService.GetBlogAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            blogfeature.BlogId = blog.Id;
            blogfeature.AppUserId = user.Id;
            blogfeature.status = true;
            blogfeature.LikeDate = DateTime.Now;
            await _blogfeature.AddAsync(blogfeature);

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> LikeBack(BlogFeature blogfeature)
        {
            await _blogFeatureService.UpdateAsync(blogfeature);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnLikeBack(BlogFeature blogfeature)
        {
            await _blogFeatureService.UpdateAsync(blogfeature);

            return RedirectToAction(nameof(Index));
        }

    }
}
