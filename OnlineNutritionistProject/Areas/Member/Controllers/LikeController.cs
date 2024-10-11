using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class LikeController : Controller
    {
        private readonly IBlogFeatureService _blogFeatureService;
        private readonly UserManager<AppUser> _userManager;

        public LikeController(IBlogFeatureService blogFeatureService, UserManager<AppUser> userManager)
        {
            _blogFeatureService = blogFeatureService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var likes = await _blogFeatureService.GetLikeListByAppUserId(user.Id);
            return View(likes);
        }
    }
}
