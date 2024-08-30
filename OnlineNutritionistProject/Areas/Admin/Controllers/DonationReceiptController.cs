using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonationReceiptController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
