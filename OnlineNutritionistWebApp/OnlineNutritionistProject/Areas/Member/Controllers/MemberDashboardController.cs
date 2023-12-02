using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    public class MemberDashboardController : Controller
    {
        [Area("Member")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
