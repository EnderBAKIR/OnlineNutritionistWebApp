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

        public async Task<IActionResult> Index()//Blogların listelendiği sayfa//List of blogs page  
        {
            return View(await _blogService.GetBlogWithNutrition());
        }


        [HttpGet]
        public async Task<IActionResult> BlogsDetails(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.AppuserId = user.Id;//for comments and likes insert
            ViewBag.BlogId = id;
            var blog = await _blogService.GetBlogAsync();//burada bloğu idsine göre detaylarını alıyoruz//here we get the details of the blog by its id

            var likeCount = await _blogFeatureService.GetLikeForAppUser(id);//burada likeları alıyoruz//here we get the likes
            ViewBag.LikeCount = likeCount.Where(x => x.status == true).Count();//burada likeları statusu true olanları alıyoruz ve sayısını alıyoruz//here we get the number of likes whose status is true

            bool doesLike = await _blogFeatureService.DoesGetLikeFilter(user.Id , blog.Id);//burada userın likeı var mı yok mu kontrol ediyoruz//here we check if the user has a like

            if (doesLike)
            {
                var featurefilter = await _blogFeatureService.GetLikeFilter(user.Id);//burada userın likeı var mı yok mu kontrol ediyoruz//here we check if the user has a like
                if (featurefilter != null)
                {
                    ViewBag.LikeId = featurefilter.Id;//burada userın likeı varsa onun id sini alıyoruz//here we get the id of the user's like if it exists
                    ViewBag.Featurefilter = featurefilter.AppUserId;//burada userın likeı varsa onun id sini alıyoruz//here we get the id of the user's like if it exists
                    ViewBag.Status = featurefilter.status;//burada userın likeı varsa onun statusunu alıyoruz//here we get the status of the user's like if it exists
                }
            }


            return View(blog);
        }

        [HttpGet]
        public IActionResult LikeCount()
        {

            return View();
        }

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
