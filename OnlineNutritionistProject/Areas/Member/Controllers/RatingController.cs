using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class RatingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPackageService _packageService;
        private readonly IRatingService _ratingService;
        private readonly IAppuserService _appuserService;
        private readonly IOrderService _orderService;

        public RatingController(UserManager<AppUser> userManager, IPackageService packageService, IRatingService ratingService, IAppuserService appuserService, IOrderService orderService)
        {
            _userManager = userManager;
            _packageService = packageService;
            _ratingService = ratingService;
            _appuserService = appuserService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> AddRating()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var nutritionist = await _appuserService.GetNutritionists();

            ViewBag.PackageNutriIds = await _packageService.GetPackageNutriIdsAsync();
            ViewBag.UserOrders = await _orderService.GetOrdersByAppUserIdAsync(user.Id);
            ViewBag.UserId = user.Id;

            //We will hire dietitians that the member will rate. // Üyenin puan vereceği diyetisyenleri alacağız.
            ViewBag.Ratings = await _ratingService.GetAllAsync();

            return View(nutritionist);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRating([FromBody] Rating rating)
        {
            await _ratingService.AddAsync(rating);
            return View();
        }
    }
}
