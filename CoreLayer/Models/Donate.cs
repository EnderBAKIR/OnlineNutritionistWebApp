﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Donate : BaseEntity
    {
        public string DonateImage { get; set; }
        public IFormFile? DonatePdf { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
