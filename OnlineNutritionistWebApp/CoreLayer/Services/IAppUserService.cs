﻿using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IAppUserService : IService<AppUser>
    {
        Task<List<AppUser>> GetAll();
    }
}
