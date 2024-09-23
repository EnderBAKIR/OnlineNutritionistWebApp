using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.ViewComponents.Package
{
    public class _LastPackage : ViewComponent
    {
        private readonly IPackageService _packageService;

        public _LastPackage(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int id)

        {
            var package = await _packageService.GetLastPackageAsync(id);
            return View(package);
        }

    }
}
