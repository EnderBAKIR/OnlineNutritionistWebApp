using System.ComponentModel.DataAnnotations;

namespace OnlineNutritionistProject.Models
{
    public class UserSignInViewModel
    {
        [Required (ErrorMessage ="Lütfen kullanıcı adınızı giriniz!")]
        public string username { get; set; }

        [Required (ErrorMessage ="Lütfen şifreinizi giriniz!")]
        public string password { get; set; }
    }
}
