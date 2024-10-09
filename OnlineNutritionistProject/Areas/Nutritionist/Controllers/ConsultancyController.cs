using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using ServiceLayer.Services;
using System.Net.WebSockets;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]
    public class ConsultancyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConsultancyService _consultancyService;

        public ConsultancyController(UserManager<AppUser> userManager, IConsultancyService consultancyService)
        {
            _userManager = userManager;
            _consultancyService = consultancyService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                var appUser = new AppUser
                {
                    AppNutriId = user.AppNutriId
                };

                var value = await _consultancyService.GetConsultancyForNutrition(appUser.AppNutriId);
                return View(value);
            }
            else
            {

                return NotFound();
            }
        }

        public async Task<IActionResult> DeleteConsultancy(int id)
        {
            var consul = await _consultancyService.GetByIdAsync(id);
            await _consultancyService.RemoveAsync(consul);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddConsultancy(int id)
        {
            var nutritionist = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.userId = nutritionist.AppNutriId;

            var consul = await _consultancyService.GetByIdAsync(id);

            return View();
        }


        public async Task<IActionResult> AddConsultancy(Consultancy consultancy)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user.AppNutriId != null)
            {
                consultancy.CreatedDate = DateTime.Now;
                await _consultancyService.AddAsync(consultancy);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
