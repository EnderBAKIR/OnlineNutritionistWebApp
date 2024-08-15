using Microsoft.AspNetCore.Http;
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

        public string? ImageUrl { get; set; }//profil fotoğrafı için  //for a profile photo
        public string? Gender { get; set; }// Cinsiyet 
        public string Name { get; set; }//Ad
        public string Surname { get; set; }//Soyad
        public string? Category { get; set; }
        public string? CertificateImage { get; set; }//sertfika doğrulama için , for Certificate confirmation
        public CertificateStatus CertificateStatus { get; set; } // Sertifika durumu

        public string? City { get; set; }//adresine göre diyetisyen seçebilmesi için , choose doctor with adress
        public bool Status { get; set; }//Status = IsNutritionist?
        public string? Description { get; set; } //Kullanıcıların kendilerine ait bilgi verdikleri alan. // The area where users provide their own information. 


        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Books> Books { get; set; }
        public ICollection<BlogFeature> BlogFeatures { get; set; }
        public Consultancy Consultancy { get; set; }
        public ICollection<GetConsultancy> GetConsultancies { get; set; }
        public ICollection<GetBooks> GetBooks { get; set; } //Kullanıcıların kitap istekleri. ////Users' book requests.

    }
    public enum CertificateStatus
    {
        Pending = 1,   // Yüklenmiş Onay Bekliyor
        Approved = 2,  // Onaylanmış
        Invalid = 3    // Geçersiz
    }
}
