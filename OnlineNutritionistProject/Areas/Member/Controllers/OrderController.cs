using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPackageService _packageService;

        public OrderController(IOrderService orderService, UserManager<AppUser> userManager, IPackageService packageService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); 
            var userOrders = await _orderService.GetOrdersByAppUserIdAsync(user.Id);

            // Paketlerin Nutri ID'lerini alıyoruz. // We get the Nutri IDs of the packages.
            var packageNutriIds = await _packageService.GetPackageNutriIdsAsync();

            ViewBag.PackageNutriIds = packageNutriIds; // Paket ID ile Nutri ID'leri eşleştiriyoruz. // We match the Package ID with the Nutri IDs.
            return View(userOrders);
        }
    }
}
