using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class DashboardPartialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
