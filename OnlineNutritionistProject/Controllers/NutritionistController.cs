using AutoMapper;
using CoreLayer.DTOs;
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
    public class NutritionistController : Controller
    {

        private readonly IAppuserService _appuserService;
        private readonly IBooksService _booksService;
        private readonly IPackageService _packageService;

        public NutritionistController(IAppuserService appuserService, IBooksService booksService, IPackageService packageService)
        {
            _appuserService = appuserService;
            _booksService = booksService;
            _packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _appuserService.GetNutritionists();
            return View(value);
        }
        public async Task<IActionResult> GetByNutriDetails(int id)
        {
            var value = await _appuserService.GetNutritionistById(id);

            var package = await _packageService.GetPacgateForNutritionist(id);
            var books = await _booksService.GetBookForNutrition(id);
            ViewBag.books = books;
            ViewBag.package = package;

            return View(value);
        }

    }
}
