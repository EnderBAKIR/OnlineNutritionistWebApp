using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class RatingController : Controller
    {
        [HttpGet]
        public IActionResult AddRating()
        {
            return View();
        }
    }
}
