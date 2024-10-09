using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class ConsultancyController : Controller
    {
        private readonly IConsultancyService _consultancyService;
        private readonly UserManager<AppUser> _userManager;

        public ConsultancyController(IConsultancyService consultancyService, UserManager<AppUser> userManager)
        {
            _consultancyService = consultancyService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> ListConsultancy()
        {
            return View(await _consultancyService.GetConsultancyWithNutrition());
        }

    }
}
