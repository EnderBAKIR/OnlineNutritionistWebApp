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

        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public string? Gender { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Category { get; set; }
        public string? CertificateImage { get; set; }//sertfika doğrulama için , for Certificate confirmation
        public CertificateStatus CertificateStatus { get; set; }// Sertifika durumu
        public string? City { get; set; }//adresine göre diyetisyen seçebilmesi için, choose doctor with adress
        public bool Status { get; set; }//Status = IsNutritionist?
        public string? Description { get; set; }
        public double CurrentWeight { get; set; }//Şuanki Kilo
        public double TargetWeight { get; set; }//Hedef Kilo

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Books> Books { get; set; }
        public ICollection<BlogFeature> BlogFeatures { get; set; }
        public Consultancy Consultancy { get; set; }
        public ICollection<GetConsultancy> GetConsultancies { get; set; }
        public ICollection<GetBooks> GetBooks { get; set; }
        public ICollection<Package> Packages { get; set; }
        public ICollection<Donate> Donates { get; set; }

    }
    public enum CertificateStatus
    {
        NotSubmitted = 0, // Yeni eklenen durum
        Pending = 1,   // Yüklenmiş Onay Bekliyor
        Approved = 2,  // Onaylanmış
        Invalid = 3    // Geçersiz
    }
}
