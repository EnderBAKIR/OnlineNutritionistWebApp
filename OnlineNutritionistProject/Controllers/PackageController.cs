using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        private readonly IBasketService _basketService;

        public PackageController(IPackageService packageService, IBasketService basketService)
        {
            _packageService = packageService;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _packageService.GetPacgateWithNutritionist());
        }

        
    }
}
