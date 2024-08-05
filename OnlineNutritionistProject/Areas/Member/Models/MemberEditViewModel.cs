using Microsoft.AspNetCore.Http;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace OnlineNutritionistProject.Areas.Member.Models
{
    public class MemberEditViewModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string mail { get; set; }
        public string phonenumber { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string username { get; set; }
        public string description { get; set; }
        public IFormFile Image  { get; set; }
      
    }
}
