using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
