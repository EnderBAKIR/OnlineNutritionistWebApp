using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class BasketController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketService _basketService;
        private readonly IPackageService _packageService;

        public BasketController(UserManager<AppUser> userManager, IBasketService basketService, IPackageService packageService)
        {
            _userManager = userManager;
            _basketService = basketService;
            _packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var basketItems = await _basketService.GetBasketByAppUserIdAsync(user.Id);
            
            return View(basketItems);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int packageId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var basketItems = await _basketService.GetBasketByAppUserIdAsync(user.Id);
            var basketItemToRemove = basketItems.FirstOrDefault(b => b.PackageIdentity == packageId);

            if (basketItemToRemove != null)
            {
                await _basketService.RemoveBasketAsync(basketItemToRemove);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}
