using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Donate : BaseEntity
    {
        public IFormFile? DonatePdf { get; set; }
        public string? DonatePdfUrl { get; set; }
        public DonationStatus DonationStatus { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
    public enum DonationStatus
    {
        NotSubmitted = 0, //Yeni yüklenmiş dekont ve geri alma işlemi //Newly uploaded receipt and Retrieval process
        Approved = 1,  //Onayla
        Invalid = 2    //Reddet
    }
}
