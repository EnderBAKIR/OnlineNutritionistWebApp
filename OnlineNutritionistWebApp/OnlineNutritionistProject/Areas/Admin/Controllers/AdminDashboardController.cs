using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminDashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
