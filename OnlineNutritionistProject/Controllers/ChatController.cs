using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{

    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPackageService _packageService;
        private readonly IAppuserService _appuserService;
        private readonly IOrderService _orderService;
        private readonly IMessageService _messageService;

        public ChatController(UserManager<AppUser> userManager, IPackageService packageService, IAppuserService appuserService, IOrderService orderService, IMessageService messageService)
        {
            _userManager = userManager;
            _packageService = packageService;
            _appuserService = appuserService;
            _orderService = orderService;
            _messageService = messageService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var nutritionist = await _appuserService.GetNutritionists();

            ViewBag.PackageNutriIds = await _packageService.GetPackageNutriIdsAsync();

            ViewBag.UserOrders = await _orderService.GetOrdersByAppUserIdAsync(user.Id);
            ViewBag.UserId = user.Id;

            return View(nutritionist);
        }

    }
}