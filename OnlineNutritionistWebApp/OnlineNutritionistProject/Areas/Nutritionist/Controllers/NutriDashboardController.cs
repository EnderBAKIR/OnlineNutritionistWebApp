using CoreLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]
    [AllowAnonymous]
    public class NutriDashboardController : Controller
    {



        private readonly UserManager<AppUser> _userManager;

        public NutriDashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.name = values.Name + " " + values.Surname;
            ViewBag.userImage = values.ImageUrl;
            ViewBag.phone = values.PhoneNumber;
            ViewBag.email = values.Email;
            ViewBag.city = values.City;
            ViewBag.userName = values.UserName;
            return View();
        }
    }
}
