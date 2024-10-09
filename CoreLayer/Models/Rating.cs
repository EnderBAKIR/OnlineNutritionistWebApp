using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Rating: BaseEntity
    {
        public int Point { get; set; }
        public int AppNutriId { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
