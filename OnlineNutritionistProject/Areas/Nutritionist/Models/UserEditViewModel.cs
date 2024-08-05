﻿namespace OnlineNutritionistProject.Areas.Nutritionist.Models
{
    public class UserEditViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Imageurl { get; set; }
        public IFormFile Image { get; set; }
        public string City { get; set; }
    }
}
