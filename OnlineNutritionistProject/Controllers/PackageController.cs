using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;

        public PackageController(IPackageService packageService, IBasketService basketService, UserManager<AppUser> userManager)
        {
            _packageService = packageService;
            _basketService = basketService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _packageService.GetPacgateWithNutritionist());
        }


        public async Task<IActionResult> AddItemBaskets(int id)
        {
            var packages = await _packageService.GetByIdAsync(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var basket = new Basket
            {
                PackageIdentity = packages.Id,
                PackageName = packages.Title,
                AppUserId = user.Id,
                Price = packages.Price,
                CreatedDate = DateTime.Now
            };
            await _basketService.CreateBasketAsync(basket);

            return RedirectToAction(nameof(Index));
        }
    }
}
