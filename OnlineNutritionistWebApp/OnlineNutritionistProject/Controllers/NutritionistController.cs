using AutoMapper;
using CoreLayer.DTOs;
using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class NutritionistController : Controller
    {

        private readonly IService<AppUser> _service;

        public NutritionistController(IService<AppUser> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
           return View(await _service.GetAllAsync());
           
        }


    }
}
