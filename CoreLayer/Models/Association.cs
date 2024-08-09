using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Association
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}
