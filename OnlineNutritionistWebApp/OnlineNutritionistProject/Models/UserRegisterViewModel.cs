using System.ComponentModel.DataAnnotations;

namespace OnlineNutritionistProject.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adını giriniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen mail adresini giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi giriniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi tekrar giriniz")]
        [Compare("Password", ErrorMessage = "şifreler uyumlu değil")]
        public string ConfirmPassword { get; set; }
		
        
        [Required(ErrorMessage = "Lütfen Kategori seçin")]
		public string Category { get; set; }

        public int AppNutriId { get; set; }



    }
}
