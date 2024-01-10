using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class GetConsultancy
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int AppuserId { get; set; }

        public AppUser AppUser { get; set; }

        public int ConsultancyId { get; set; }

        public Consultancy Consultancy { get; set; }
    }
}
