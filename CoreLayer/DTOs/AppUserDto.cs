using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
    public class AppUserDto
    {
        public string? ImageUrl { get; set; }//profil fotoğrafı için , profile Image
        public string? Gender { get; set; }
        public string Name { get; set; }//Ad
        public string Surname { get; set; }//Soyad
        public string? Category { get; set; }
        public string? City { get; set; }//adresine göre diyetisyen seçebilmesi için , choose doctor with adress
    }
}
