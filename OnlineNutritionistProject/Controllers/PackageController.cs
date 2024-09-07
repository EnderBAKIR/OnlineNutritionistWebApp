using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _packageService.GetPacgateWithNutritionist());
        }
    }
}
