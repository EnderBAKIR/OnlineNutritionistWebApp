using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    public class NutriDashboardController : Controller
    {
        [Area("Nutritionist")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
