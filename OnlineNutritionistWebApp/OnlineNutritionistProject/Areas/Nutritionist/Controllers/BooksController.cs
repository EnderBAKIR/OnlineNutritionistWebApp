﻿using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]
    public class BooksController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBooksService _booksService;

        public BooksController(UserManager<AppUser> userManager, IBooksService booksService)
        {
            _userManager = userManager;
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> ListBooks()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                var appUser = new AppUser
                {
                    Id = user.Id
                };




                var value = await _booksService.GetBookForNutrition(appUser.Id);

                return View(value);
            }
            else
            {

                return NotFound();
            }
        }


        public async Task<IActionResult> DeleteBook(int id)
        {
            var value = await _booksService.GetByIdAsync(id);
            await _booksService.RemoveAsync(value);
            return RedirectToAction("ListBooks");

        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.userId = user.Id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Books books)
        {


            if (books.ImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(books.ImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/booksimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await books.ImageUrl.CopyToAsync(stream);
                books.Image = imagename;
            }
           



            books.CreatedDate = DateTime.Now;

            await _booksService.AddAsync(books);

            return RedirectToAction("ListBooks");
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {


            var value = await _booksService.GetByIdAsync(id);

            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(Books books)
        {

            if (books.ImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(books.ImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/booksimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await books.ImageUrl.CopyToAsync(stream);
                books.Image = imagename;
            }
           




            books.CreatedDate = DateTime.Now;

            await _booksService.UpdateAsync(books);

            return RedirectToAction("ListBooks");
        }
    }
}