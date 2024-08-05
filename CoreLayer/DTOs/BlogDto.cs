using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
    public class BlogDto:BaseEntitiyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public String AppUserUserName { get; set; }//İdentity kütüphanesi standart username adlandırması //Identity Library default username title.
    }
}
