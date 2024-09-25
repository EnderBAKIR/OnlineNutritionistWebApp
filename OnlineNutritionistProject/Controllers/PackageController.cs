using CoreLayer.Models;
using CoreLayer.Services;
using Iyzipay.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userBasket = await _basketService.GetBasketByAppUserIdAsync(user.Id);
            ViewBag.UserBasket = userBasket;
            return View(await _packageService.GetPacgateWithNutritionist());
        }

        public async Task<IActionResult> PackageDetail(int id)
        {
            var packagedetail = await _packageService.GetPackageDetailAsync(id);
            TempData["packageId"] = packagedetail.Id;
            return View(packagedetail);
        }


        //For Index
        public async Task<IActionResult> AddItemBasketsinIndex(int id)
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

        public async Task<IActionResult> RemoveFromBasketinIndex(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var basketItems = await _basketService.GetBasketByAppUserIdAsync(user.Id);
            var basketItemToRemove = basketItems.FirstOrDefault(b => b.PackageIdentity == id);
            await _basketService.RemoveBasketAsync(basketItemToRemove);
            return RedirectToAction(nameof(Index));
        }


        // For Details
        public async Task<IActionResult> AddItemBasketsinDetails()
        {
            int packageId = Convert.ToInt32(TempData["packageId"]);
            var packages = await _packageService.GetByIdAsync(packageId);
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

            return RedirectToAction("PackageDetail", "Package", new { id = packageId });
        }

        public async Task<IActionResult> RemoveFromBasketinDetails()
        {
            int packageId = Convert.ToInt32(TempData["packageId"]);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var basketItems = await _basketService.GetBasketByAppUserIdAsync(user.Id);
            var basketItemToRemove = basketItems.FirstOrDefault(b => b.PackageIdentity == packageId);
            await _basketService.RemoveBasketAsync(basketItemToRemove);
            return RedirectToAction("PackageDetail" , "Package" , new { id = packageId });
        }
    }
}
