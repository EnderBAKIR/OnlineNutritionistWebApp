using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Package: BaseEntity
    {
        public bool Remove { get; set; } //The dietitian will be able to make her package active or passive whenever she wants.
        public bool Suspend { get; set; } //Admin will be able to suspend inappropriate packages.
        public string? Image { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
