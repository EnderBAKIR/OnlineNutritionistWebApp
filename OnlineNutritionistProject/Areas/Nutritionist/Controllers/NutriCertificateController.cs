using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]
    public class NutriCertificateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
