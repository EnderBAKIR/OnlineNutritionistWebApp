using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Basket : BaseEntity
    {
        public string PackageName { get; set; }
        public int PackageIdentity { get; set; }
        public decimal Price { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
