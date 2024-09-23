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

        public OrderController(IOrderService orderService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Kullanıcının siparişlerini al
            var userOrders = await _orderService.GetOrdersByAppUserIdAsync(user.Id);

            // View'e siparişleri gönder
            return View(userOrders);
        }
    }
}
