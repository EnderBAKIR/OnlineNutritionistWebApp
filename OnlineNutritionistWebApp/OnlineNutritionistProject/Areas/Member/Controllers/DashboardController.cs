using CoreLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [AllowAnonymous]
    [Area("Member")]
    public class DashboardController : Controller
    {
      private readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.memberName = values.Name + " " + values.Surname;
            ViewBag.UserImage = values.ImageUrl;
            ViewBag.userName = values.UserName;
            return View();
        }
    }
}
