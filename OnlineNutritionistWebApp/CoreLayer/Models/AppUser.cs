using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class AppUser : IdentityUser<int>
        
    {
        public int? AppNutriId { get; set; }//eğer statusu true ise bu id de ekstra olarak kullanılacak , erişim belirleyicisi ve ana anahtar sütunu değil
        //Consultancy işlemleri için gerekli /// If status == true {AppNutriId ++}; , AppNutriId isn't foreign key or haskey

        public string? ImageUrl { get; set; }//profil fotoğrafı için , profile Image
        public string? Gender { get; set; }
        public string Name { get; set; }//Ad
        public string Surname { get; set; }//Soyad
        public string? Category { get; set; }
        public string? CertificateImage { get; set; }//sertfika doğrulama için , for Certificate confirmation
        public string? City { get; set; }//adresine göre diyetisyen seçebilmesi için , choose doctor with adress
        public bool Status { get; set; }//Status = IsNutritionist?
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Consultancy Consultancy { get; set; }
        public ICollection<GetConsultancy> GetConsultancies { get; set; }

    }
}
