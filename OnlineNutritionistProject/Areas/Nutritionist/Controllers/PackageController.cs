using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]

    public class PackageController : Controller
    {
        private readonly IPackageService _service;

        public PackageController(IPackageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListPackages()
        {
            return View(await _service.GetAllAsync());
        }

    }
}
