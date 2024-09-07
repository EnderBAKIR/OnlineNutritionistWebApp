using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class PackageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
