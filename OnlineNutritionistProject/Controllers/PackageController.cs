using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    public class PackageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
