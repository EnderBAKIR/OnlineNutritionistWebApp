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

        public NutritionistController(IAppuserService appuserService)
        {
            _appuserService = appuserService;
        }

        public async Task<IActionResult> Index(AppUser appuser)
        {
            
            
                var value = await _appuserService.GetNutritionists();
                return View(value);
            
           
        }
        public async Task<IActionResult> GetBlogsByNutri(int id)
        {
            var value = await _appuserService.GetNutritionistById(id);
            return View(value);
        }

    }
}
