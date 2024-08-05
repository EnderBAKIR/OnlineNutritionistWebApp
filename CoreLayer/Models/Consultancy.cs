using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Consultancy :BaseEntity
    {
        public int AppUserId { get; set; }
        
        public AppUser AppUser { get; set; }
        public bool status { get; set; }
        public string Description { get; set; }

        public ICollection<GetConsultancy> GetConsultancies { get; set; }
    }
}
