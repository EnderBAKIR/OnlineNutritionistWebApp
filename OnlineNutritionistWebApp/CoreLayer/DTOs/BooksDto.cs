using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
    public class BooksDto:BaseEntitiyDto
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
